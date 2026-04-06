using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static DataBucketExtensions;

public class DataBucketController
{
    public static DataBucketController Instance {
        get
        {
            _instance ??= new DataBucketController();
            return _instance;
        }
    }

    private static DataBucketController _instance;

    private DataBucketRuntime _dataBucket;
    private IDataBucketMapper _dataBucketMapper;
    public IDataBucketMapper DataBucketMapper => _dataBucketMapper;

    // Dependencies
    private IDataBucketConfig _config;
    private IDataBucketStorage _storage;
    private IDataBucketExperimentProvider _experimentProvider;
    private IDataBucketAttributionProvider _attributionProvider;
    private ICountryCodeProvider _countryCodeProvider;
    private IDataBucketLogger _logger;
    private IDatabucketTrackerProvider _trackerProvider;

    private Dictionary<string, object> _debugProperties = new Dictionary<string, object>();

    public Dictionary<string, object> DictDebugProperties
    {
        get
        {
            return _debugProperties;
        }
    }

    public void InitDataBucket(IDataBucketMapper dataBucketMapper, IDataBucketConfig config)
    {
        InitDataBucket(
            dataBucketMapper, 
            config, 
            new UnityDataBucketStorage(), 
            new FirebaseExperimentProvider(), 
            new AdjustAttributionProvider(), 
            new UnityWebCountryCodeProvider(), 
            new UnityDataBucketLogger(), 
            new DatabucketTrackerProvider()
        );
    }

    public void InitDataBucket(
        IDataBucketMapper dataBucketMapper,
        IDataBucketConfig config,
        IDataBucketStorage storage,
        IDataBucketExperimentProvider experimentProvider,
        IDataBucketAttributionProvider attributionProvider,
        ICountryCodeProvider countryCodeProvider,
        IDataBucketLogger logger,
        IDatabucketTrackerProvider trackerProvider)
    {
        _dataBucketMapper = dataBucketMapper;
        _config = config;
        _storage = storage;
        _experimentProvider = experimentProvider;
        _attributionProvider = attributionProvider;
        _countryCodeProvider = countryCodeProvider;
        _logger = logger;
        _trackerProvider = trackerProvider;

        _dataBucket = Load();
        _ = TaskInitDataBucket();
    }

    public void HandleGameEvent(string eventName, object payload = null)
    {
        switch (eventName)
        {
            case DataBucketEngineEvents.StageLoadStart:
                _dataBucket.durationStageLoad = Stopwatch.StartNew();
                break;
            case DataBucketEngineEvents.StagePlayStart:
                _dataBucket.durationStagePlay = Stopwatch.StartNew();
                _dataBucket.currentModeTracker.EnterLevel(_dataBucketMapper.CurrentMapSelect);
                Save();
                break;
            case DataBucketEngineEvents.StayIAPStart:
                _dataBucket.durationStayIAP = Stopwatch.StartNew();
                break;
            case DataBucketEngineEvents.AdRequestStart:
                _dataBucket.durationAdRequest = Stopwatch.StartNew();
                break;
            case DataBucketEngineEvents.AdCompleteStart:
                _dataBucket.durationAdComplete = Stopwatch.StartNew();
                break;
            case DataBucketEngineEvents.LoseLevel:
                _dataBucket.currentModeTracker.LoseLevel(_dataBucketMapper.CurrentMapSelect);
                Save();
                break;
            case DataBucketEngineEvents.IncreaseDayActiveCount:
                _dataBucket.dayActiveCount += 1;
                SetProperty("active_day", _dataBucket.dayActiveCountForDataBucket);
                Save();
                break;
            case DataBucketEngineEvents.SetPropertyCurrentMapSelect:
                SetProperty("stage_current", _dataBucketMapper.PropertiesDataBucketIn.StageCurrent);
                break;
        }
    }
    
    private async Task TaskInitDataBucket()
    {
        await Task.Delay(TimeSpan.FromSeconds(_config.InitDelaySeconds));
        _logger.Log($"[DataBucket] InitDataBucket!");
        _trackerProvider.Init(_config.ApiEndpoint, _config.ApiKey);

        await IEFirstSyncUserState();

        // Track app start
        _trackerProvider.Record("app_started", null);
    }
    
    private async Task IEFirstSyncUserState()
    {
        // Removed wait for DataManager.Instance == null to keep decoupling. 
        // Sync operation happens based on provided DataBucketMapper.
        _logger.Log($"[DataBucket] IEFirstSyncUserState!");

        SetProperty("user_id", _dataBucket.databucketUUID);
        SetProperty("active_day", _dataBucket.dayActiveCountForDataBucket);

        SetProperty("stage_current", _dataBucketMapper.PropertiesDataBucketIn.StageCurrent);
        SetProperty("stage_max", _dataBucketMapper.PropertiesDataBucketIn.StageMax);
        SetProperty("player_name", _dataBucketMapper.PropertiesDataBucketIn.PlayerName);
        SetProperty("player_level", _dataBucketMapper.PropertiesDataBucketIn.PlayerLevel);

        SyncABTesting();

        _attributionProvider.FetchAttribution((attribution) =>
        {
            if (attribution != null)
            {
                if (!string.IsNullOrEmpty(attribution.Network)) SetProperty("ua_network", attribution.Network);
                if (!string.IsNullOrEmpty(attribution.Campaign)) SetProperty("ua_campaign", attribution.Campaign);
                if (!string.IsNullOrEmpty(attribution.Adgroup)) SetProperty("ua_adgroup", attribution.Adgroup);
                if (!string.IsNullOrEmpty(attribution.Creative)) SetProperty("ua_creative", attribution.Creative);
                if (!string.IsNullOrEmpty(attribution.TrackerName)) SetProperty("ua_tracker_name", attribution.TrackerName);
            }
        });

        _countryCodeProvider.GetCountryCodeAsync((code) =>
        {
            // Cached if needed dynamically, currently just sets properties if needed.
        });
    }

    public void SyncABTesting()
    {
        if (_experimentProvider == null) return;

        List<string> experiments = _experimentProvider.GetActiveExperiments();

        if (experiments == null || experiments.Count == 0)
        {
            _logger.Log("[Experiment] null");
            SetProperty("firebase_exp", "null");
            return;
        }

        string[] experimentArray = experiments.ToArray();
        _logger.Log("[Experiment] " + string.Join(", ", experimentArray));
        SetProperty("firebase_exp", experimentArray);
    }

    private long GetInstallTime()
    {
        if (!_storage.HasKey("install_time"))
        {
            long time = System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            _storage.SetString("install_time", time.ToString());
            return time;
        }
        return long.Parse(_storage.GetString("install_time"));
    }

    public void SetProperty(string key, object value)
    {
        _logger.Log($"[Databucket] SetProperty: {key} | {value}");
        _trackerProvider.SetCommonProperty(key, value);

        if (_debugProperties.ContainsKey(key))
        {
            _debugProperties[key] = value;
        }
        else
        {
            _debugProperties.Add(key, value);
        }
    }

    public void SetProperties(Dictionary<string, object> @params)
    {
        string msg = "[Databucket] SetProperties: \n";
        foreach (var item in @params)
        {
            msg += $"{item.Key} | {item.Value}";
        }
        _logger.Log(msg);

        _trackerProvider.SetCommonProperties(@params);

        foreach (var item in @params)
        {
            if (_debugProperties.ContainsKey(item.Key))
            {
                _debugProperties[item.Key] = item.Value;
            }
            else
            {
                _debugProperties.Add(item.Key, item.Value);
            }
        }
    }

    public void LogEvent(string key, Dictionary<string, object> @params)
    {
        string msg = $"[Databuckets] LogEvent {key} -> ";
        if (@params != null && @params.Count > 0)
        {
            msg += string.Join("| ", @params.Select(kv => $"{kv.Key}: {kv.Value}"));
        }
        else
        {
            msg += "No Params";
        }
        _logger.Log(msg);

        _trackerProvider.Record(key, @params);
    }

    public void OnUserLogout()
    {
        _trackerProvider.Record("user_logout", null);
        _trackerProvider.ForceEndCurrentSession();
    }

    #region Story Mode

    public void LogLevelStart()
    {
        try
        {
            if (_dataBucket.durationStageLoad == null)
            {
                _logger.LogError("Missing duration stage load");
                return;
            }

            var gamePlay = _dataBucketMapper.GamePlay;
            var levelDataBucketIn = _dataBucketMapper.LevelPlayDataBucketIn;
            var mode = _dataBucketMapper.Mode;
            
            var (playIndex, loseIndex, exitIndex, currentMapSelect, isReplayAfterWin) = GetDetailLevelPlay();
            var difficulty = levelDataBucketIn.Difficulty;
            long durationStageLoad = _dataBucket.durationStageLoad.ElapsedMilliseconds;
            var infoEquipment = _dataBucket.LogEquipmentsInfo(levelDataBucketIn.DroneLeft, levelDataBucketIn.DroneRight);
            var waveTotal = gamePlay.WaveTotal;
            
            _dataBucket.durationStageLoad.Stop();
            
            LogEvent(DataBucketConstant.EventName.LevelStart,
                DataBucketBuilder
                    .CreateLevelStart(currentMapSelect, playIndex, loseIndex, isReplayAfterWin, durationStageLoad, mode,
                        difficulty.ToString(), infoEquipment, waveTotal, levelDataBucketIn.Ship, levelDataBucketIn.DroneLeft, levelDataBucketIn.DroneRight)
                    .ToDictionary());
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
        }
    }
    
    public void LogLevelEnter()
    {
        try
        {
            var levelDataBucketIn = _dataBucketMapper.LevelPlayDataBucketIn;
            var mode = _dataBucketMapper.Mode;
            
            var infoEquipment = _dataBucket.LogEquipmentsInfo(levelDataBucketIn.DroneLeft, levelDataBucketIn.DroneRight);
            var (playIndex, loseIndex, exitIndex, currentMapSelect, isReplayAfterWin) = GetDetailLevelPlay();
            var difficulty = levelDataBucketIn.Difficulty;
            
            LogEvent(DataBucketConstant.EventName.LevelEnter,
                DataBucketBuilder
                    .CreateLevelEnter(currentMapSelect, playIndex, loseIndex, isReplayAfterWin, mode,
                        difficulty.ToString(), infoEquipment, levelDataBucketIn.Ship, levelDataBucketIn.DroneLeft, levelDataBucketIn.DroneRight)
                    .ToDictionary());
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
        }
    }

    public void LogLevelEnd()
    {
        try
        {
            if (_dataBucket.durationStagePlay == null)
            {
                _logger.LogError("Missing duration stay play");
                return;
            }
            
            var levelDataBucketIn = _dataBucketMapper.LevelPlayDataBucketIn;
            var gamePlay = _dataBucketMapper.GamePlay;
            var mode = _dataBucketMapper.Mode;
            
            var durationStagePlay = _dataBucket.durationStagePlay.ElapsedMilliseconds;
            var (playIndex, loseIndex, exitIndex, currentMapSelect, isReplayAfterWin) = GetDetailLevelPlay();
            
            var waveCleared = gamePlay.WaveCleared;
            var waveTotal = gamePlay.WaveTotal;
            var liveRemain = gamePlay.LiveRemain;
            var isWin = gamePlay.IsWin;
            var score = gamePlay.Score;

            var result = HandleLevelResult(isWin, gamePlay.DidQuitByUser);
            var loseBy = HandelLevelLoseBy(isWin, gamePlay.DidQuitByUser);
            var infoEquipment = _dataBucket.LogEquipmentsInfo(levelDataBucketIn.DroneLeft, levelDataBucketIn.DroneRight);
            var infoGun = _dataBucket.LogGunInfo(levelDataBucketIn.Gun);

            _dataBucket.durationStagePlay.Stop();

            LogEvent(DataBucketConstant.EventName.LevelEnd, DataBucketBuilder
                .CreateLevelEnd(currentMapSelect, playIndex, loseIndex, isReplayAfterWin, durationStagePlay, waveCleared,
                    waveTotal, (int?)liveRemain, result, loseBy, mode,
                    levelDataBucketIn.Difficulty.ToString(), infoEquipment, infoGun, score, levelDataBucketIn.Ship, levelDataBucketIn.DroneLeft, levelDataBucketIn.DroneRight)
                .ToDictionary());
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
        }
    }

    public void LogLevelExit()
    {
        try
        {
            var gamePlay = _dataBucketMapper.GamePlay;
            var levelDataBucketIn = _dataBucketMapper.LevelPlayDataBucketIn;
            var mode = _dataBucketMapper.Mode;

            var durationStagePlay = _dataBucket.durationStagePlay.ElapsedMilliseconds;
            var (playIndex, loseIndex, exitIndex, currentMapSelect, isReplayAfterWin) = GetDetailLevelPlay();

            var waveCleared = gamePlay.WaveCleared;
            var waveTotal = gamePlay.WaveTotal;
            var liveRemain = gamePlay.LiveRemain;
            var infoEquipment = _dataBucket.LogEquipmentsInfo(levelDataBucketIn.DroneLeft, levelDataBucketIn.DroneRight);
            var infoGun = _dataBucket.LogGunInfo(levelDataBucketIn.Gun);
            
            LogEvent(DataBucketConstant.EventName.LevelExit, DataBucketBuilder
                .CreateLevelExit(currentMapSelect, playIndex, loseIndex, exitIndex, isReplayAfterWin, durationStagePlay,
                    waveCleared, waveTotal, (int)liveRemain, mode, levelDataBucketIn.Difficulty.ToString(), infoEquipment, infoGun,
                    levelDataBucketIn.Ship, levelDataBucketIn.DroneLeft, levelDataBucketIn.DroneRight)
                .ToDictionary());
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
        }
    }
    
    public void LogLevelReopen()
    {
        try
        {
            var levelDataBucketIn = _dataBucketMapper.LevelPlayDataBucketIn;
            var dataBucket = _dataBucket;
            var mode = _dataBucketMapper.Mode;
            var infoEquipment = dataBucket.LogEquipmentsInfo(levelDataBucketIn.DroneLeft, levelDataBucketIn.DroneRight);
            var (playIndex, loseIndex, exitIndex, currentMapSelect, isReplayAfterWin) = GetDetailLevelPlay();
            var difficulty = levelDataBucketIn.Difficulty;

            LogEvent(DataBucketConstant.EventName.LevelReopen, DataBucketBuilder
                .CreateLevelReOpen(currentMapSelect, playIndex, loseIndex, isReplayAfterWin, mode,
                    difficulty.ToString(), infoEquipment, levelDataBucketIn.Ship, levelDataBucketIn.DroneLeft,
                    levelDataBucketIn.DroneRight)
                .ToDictionary());
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
        }
    }

    private (int, int, int, int, string) GetDetailLevelPlay()
    {
        var currentModeTracker = _dataBucket.currentModeTracker;

        var currentMapSelect = _dataBucketMapper.CurrentMapSelect;
        var isReplayAfterWin = currentModeTracker.IsPassedMap(currentMapSelect) ? "yes" : "no";
        var playIndex = currentModeTracker.GetPlayIndexAt(currentMapSelect);
        var loseIndex = currentModeTracker.GetLoseIndexAt(currentMapSelect);
        var exitIndex = currentModeTracker.ExitIndex;

        return (playIndex, loseIndex, exitIndex, currentMapSelect, isReplayAfterWin);
    }

    private string HandleLevelResult(bool? isWin, bool? didQuitByUser)
    {
        var result = "";
        if (isWin == null)
            return null;
        
        if (isWin == true)
            result = "win";
        else if (didQuitByUser == true)
            result = "quit";
        else
            result = "lose";

        return result;
    }

    private string HandelLevelLoseBy(bool? isWin, bool? didQuitByUser)
    {
        var result = "";
        
        if (isWin == null)
            return null;
        
        if (isWin == true)
            result = "null";
        else if (didQuitByUser == true)
            result = "quit";
        else
            result = "out_of_live";

        return result;
    }

    public void HandleFlowUserExitGame(bool lastPauseStatus)
    {
        var dataBucket = _dataBucket;
        var currentModeTracker = dataBucket.currentModeTracker;

        if (lastPauseStatus)
        {
            LogLevelExit();
            currentModeTracker.ExitIndex++;
            Save();
        }
        else
        {
            LogLevelReopen();
        }
    }
    
    #endregion
   
    #region Resources

    public void LogResourcesEarn(string name, string type, float amount, ResourceLogParams resourceLog)
    {
        try
        {
            if (resourceLog == null) return;
            LogEvent(DataBucketConstant.EventName.ResourceEarn,
                DataBucketBuilder.CreateResourceEarn(name, type, amount, resourceLog.resourcesDetails,
                        resourceLog.placement, resourceLog.placementDetail, resourceLog.reason)
                    .ToDictionary());
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
        }
    }

    public void LogResourcesSpend(string name, string type, float amount, ResourceLogParams resourceLog)
    {
        try
        {
            if (resourceLog == null) return;
            LogEvent(DataBucketConstant.EventName.ResourceSpend,
                DataBucketBuilder.CreateResourceSpend(name, type, amount, resourceLog.resourcesDetails,
                        resourceLog.placement, resourceLog.placementDetail, resourceLog.reason)
                    .ToDictionary());
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
        }
    }

    #endregion

    #region IAP

    public void LogIAPShow(string placementId, DataBucketConstant.IAPAnalytics.ShowType showType,
        DataBucketConstant.IAPAnalytics.TriggerType triggerType, string packName = "null", string customPlacement = "")
    {
        try
        {
            HandleGameEvent(DataBucketEngineEvents.StayIAPStart);
            LogEvent(DataBucketConstant.EventName.IAPShow,
                DataBucketBuilder.CreateIAPShow(placementId, showType, triggerType, packName, customPlacement)
                    .ToDictionary());
        }
        catch (Exception e)
        {
            _logger.LogException(e);
        }
    }

    public void LogIAPClick(string placementId, DataBucketConstant.IAPAnalytics.ShowType showType,
        DataBucketConstant.IAPAnalytics.TriggerType triggerType, string packName, string customPlacement = "")
    {
        try
        {
            LogEvent(DataBucketConstant.EventName.IAPClick,
                DataBucketBuilder.CreateIAPClick(placementId, showType, triggerType, packName, customPlacement)
                    .ToDictionary());
        }
        catch (Exception e)
        {
           _logger.LogException(e);
        }
    }

    public void LogIAPPurchase(string placementId, DataBucketConstant.IAPAnalytics.ShowType showType,
        DataBucketConstant.IAPAnalytics.TriggerType triggerType, string packName, float price, string currency = "USD",
        string customPlacement = "")
    {
        try
        {
            LogEvent(DataBucketConstant.EventName.IAPPurchase,
                DataBucketBuilder.CreateIAPPurchase(placementId, showType, triggerType, packName, price, currency,
                        customPlacement)
                    .ToDictionary());
        }
        catch (Exception e)
        {
            _logger.LogException(e);
        }
    }

    public void LogIAPClose(string placementId, DataBucketConstant.IAPAnalytics.ShowType showType,
        DataBucketConstant.IAPAnalytics.TriggerType triggerType, string packName = "null")
    {
        try
        {
            if (_dataBucket.durationStayIAP == null)
            {
                _logger.LogError("Missing duration stay IAP");
                return;
            }
        
            LogEvent(DataBucketConstant.EventName.IAPClose,
                DataBucketBuilder.CreateIAPClose(placementId, showType, triggerType, packName,
                        _dataBucket.durationStayIAP.ElapsedMilliseconds)
                    .ToDictionary());

            _dataBucket.durationStayIAP.Stop();
            _dataBucket.durationStayIAP = null;
        }
        catch (Exception e)
        {
           _logger.LogException(e);
        }
    }
    
    #endregion

    #region Ads

    public void LogAdRequest(DataBucketConstant.AdsAnalytics.AdFormat adFormat, string placement, string adUnitId,
        bool isLoaded, IAdInfo adInfo)
    {
        try
        {
            if (_dataBucket.durationAdRequest == null)
            {
                _logger.LogError("Missing duration ad request");
                return;
            }
            
            var loadTime = _dataBucket.durationAdRequest.ElapsedMilliseconds;
            
            LogEvent(DataBucketConstant.EventName.AdRequest,
                DataBucketBuilder.CreateAdRequest(adFormat, adInfo.AdPlatForm, adInfo.NetworkName, adUnitId, placement,
                        isLoaded, loadTime)
                    .ToDictionary());
        
            _dataBucket.durationAdRequest.Stop();
            _dataBucket.durationAdRequest = null;
        }
        catch (Exception e)
        {
            _logger.LogException(e);
        }
    }

    public void LogAdImpression(DataBucketConstant.AdsAnalytics.AdFormat adFormat, string placement, string adUnitId,
        bool isShow, float value, IAdInfo adInfo)
    {
        try
        {
            LogEvent(DataBucketConstant.EventName.AdImpression,
                DataBucketBuilder.CreateAdImpression(adFormat, adInfo.AdPlatForm, adInfo.NetworkName, adUnitId,
                        placement,
                        isShow, value)
                    .ToDictionary());
        }
        catch (Exception e)
        {
            _logger.LogException(e);
        }
    }

    public void LogAdClick(DataBucketConstant.AdsAnalytics.AdFormat adFormat, string placement, string adUnitId,
        IAdInfo adInfo)
    {
        try
        {
            LogEvent(DataBucketConstant.EventName.AdClick,
                DataBucketBuilder.CreateAdClick(adFormat, adInfo.AdPlatForm, adInfo.NetworkName, adUnitId,
                        placement)
                    .ToDictionary());
        }
        catch (Exception e)
        {
            _logger.LogException(e);
        }
    }

    public void LogAdComplete(DataBucketConstant.AdsAnalytics.AdFormat adFormat, string placement,
        string adUnitId, DataBucketConstant.AdsAnalytics.EndType endType, IAdInfo adInfo)
    {
        try
        {
            if (_dataBucket.durationAdComplete == null)
            {
                _logger.LogError("Missing duration ad complete");
                return;
            }
            
            var adDuration = _dataBucket.durationAdComplete.ElapsedMilliseconds;
            
            LogEvent(DataBucketConstant.EventName.AdComplete,
                DataBucketBuilder.CreateAdComplete(adFormat, adInfo.AdPlatForm, adInfo.NetworkName, adUnitId,
                        placement, adDuration, endType)
                    .ToDictionary());
            
            _dataBucket.durationAdComplete.Stop();
            _dataBucket.durationAdComplete = null;
        }
        catch (Exception e)
        {
            _logger.LogException(e);
        }
    }

    #endregion

    #region Live Ops

    public string KEY_FEATURE_OPEN_INDEX = "FEATURE_OPEN_IDX_{0}";
    public Dictionary<string, float> _dictTimeOpenFeature = new Dictionary<string, float>();

    public void LogFeatureFistShow(string featureName, string placement)
    {
        Dictionary<string, object> _dict = new Dictionary<string, object>()
        {
            { "feature_name", featureName },
            { "placement", placement }
        };
        DataBucketController.Instance.LogEvent("feature_first_show", _dict);
    }

    #endregion
    
    #region Tutorial

    public void LogTutorialAction(DataBucketConstant.Other.ActionCategory category, string actionName, int actionIndex)
    {
        try
        {
            LogEvent(DataBucketConstant.EventName.TutorialAction,
                DataBucketBuilder.CreateTutorialAction(category, actionName, actionIndex)
                    .ToDictionary());
        }
        catch (Exception e)
        {
            _logger.LogException(e);
        }
    }
    #endregion
    
    #region Load/Save

    private const string Path = "DataBucket";
    
    public DataBucketRuntime Load()
    {
        var content = _storage.GetString(Path, "{}");
        _dataBucket = JsonConvert.DeserializeObject<DataBucketRuntime>(content);
        
        _dataBucket ??= new DataBucketRuntime();
        _dataBucket.ValidateData(
            totalStory: _dataBucketMapper.TotalMapStoryMode,
            totalBoss: _dataBucketMapper.TotalMapBossRaid,
            totalEndless: _dataBucketMapper.TotalMapEndless,
            totalTutorial: _dataBucketMapper.TotalTutorial,
            logger: _logger
        );

        Save();
        
        return _dataBucket;
    }

    public void Save()
    {
        var content = JsonConvert.SerializeObject(_dataBucket);
        _storage.SetString(Path, content);
    }
    #endregion
}
