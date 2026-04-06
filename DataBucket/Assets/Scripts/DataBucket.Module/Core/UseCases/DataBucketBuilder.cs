using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;
using static DataBucketExtensions;

public static class DataBucketBuilder
{
    public static BaseDataBucketModel CreateLevelEnter(int stage, int playIndex, int loseIndex,
        string isReplayAfterWin, PlayModeLevelDataBucket mode, string difficulty, string infoEquipment,
        EntityComponent? ship, EntityComponent? droneLeft, EntityComponent? droneRight)
    {
        var (shipId, shipLevel,
            droneLeftId, droneLeftLevel,
            droneLeftRank, droneRightId,
            droneRightLevel, droneRightRank) = GetInfoShip(ship, droneLeft, droneRight);

        return new LevelEnterDataBucketModel()
        {
            Stage = stage,
            PlayIndex = playIndex,
            LoseIndex = loseIndex,
            IsReplayAfterWin = isReplayAfterWin,
            
            ShipId = shipId,
            ShipLevel = shipLevel,

            DroneLeftId = droneLeftId,
            DroneLeftLevel = droneLeftLevel,
            DroneLeftRank = droneLeftRank,

            DroneRightId = droneRightId,
            DroneRightLevel = droneRightLevel,
            DroneRightRank = droneRightRank,
            
            InfoEquipment = infoEquipment,
            Mode = mode.ToString(),
            Difficulty = difficulty,
        };
    }
    
    public static BaseDataBucketModel CreateLevelStart(int stage, int playIndex, int loseIndex,
        string isReplayAfterWin, long msLoad, PlayModeLevelDataBucket mode, string difficulty, string infoEquipment, int? waveTotal,
        EntityComponent? ship, EntityComponent? droneLeft, EntityComponent? droneRight)
    {
        var (shipId, shipLevel,
            droneLeftId, droneLeftLevel,
            droneLeftRank, droneRightId,
            droneRightLevel, droneRightRank) = GetInfoShip(ship, droneLeft, droneRight);

        return new LevelStartDataBucketModel()
        {
            Stage = stage,
            PlayIndex = playIndex,
            LoseIndex = loseIndex,
            IsReplayAfterWin = isReplayAfterWin,
            
            ShipId = shipId,
            ShipLevel = shipLevel,

            DroneLeftId = droneLeftId,
            DroneLeftLevel = droneLeftLevel,
            DroneLeftRank = droneLeftRank,

            DroneRightId = droneRightId,
            DroneRightLevel = droneRightLevel,
            DroneRightRank = droneRightRank,
            
            InfoEquipment = infoEquipment,
            Mode = mode.ToString(),
            Difficulty = difficulty,
            MsLoad = msLoad,
            
            WaveTotal = waveTotal,
        };
    }
    
    public static BaseDataBucketModel CreateLevelEnd(int stage, int playIndex, int loseIndex, string isReplayAfterWin,
        long msDurationPlay, int? waveCleared, int? waveTotal, int? liveRemain, string result, string loseBy,
        PlayModeLevelDataBucket mode, string difficulty, string infoEquipment, string infoGun, int? score,
        EntityComponent? ship, EntityComponent? droneLeft, EntityComponent? droneRight)
    {
        var (shipId, shipLevel,
            droneLeftId, droneLeftLevel,
            droneLeftRank, droneRightId,
            droneRightLevel, droneRightRank) = GetInfoShip(ship, droneLeft, droneRight);

        return new LevelEndDataBucketModel()
        {
            Stage = stage,
            Mode = mode.ToString(),
            Difficulty = difficulty,
            PlayIndex = playIndex,
            LoseIndex = loseIndex,
            MsPlay = msDurationPlay,

            IsReplayAfterWin = isReplayAfterWin,
            ShipId = shipId,
            ShipLevel = shipLevel,

            DroneLeftId = droneLeftId,
            DroneLeftLevel = droneLeftLevel,
            DroneLeftRank = droneLeftRank,

            DroneRightId = droneRightId,
            DroneRightLevel = droneRightLevel,
            DroneRightRank = droneRightRank,

            InfoEquipment = infoEquipment,
            InfoGun = infoGun,

            WaveCleared = waveCleared,
            WaveTotal = waveTotal,
            LiveRemain = liveRemain,

            Result = result,
            Score = score,
            LoseBy = loseBy,
        };
    }

    public static BaseDataBucketModel CreateLevelExit(int stage, int playIndex, int loseIndex, int exitIndex,
        string isReplayAfterWin,
        long msDurationPlay, int? waveCleared, int? waveTotal, int? liveRemain,
        PlayModeLevelDataBucket mode, string difficulty, string infoEquipment, string infoGun,
        EntityComponent? ship, EntityComponent? droneLeft, EntityComponent? droneRight)
    {
        var (shipId, shipLevel,
            droneLeftId, droneLeftLevel,
            droneLeftRank, droneRightId,
            droneRightLevel, droneRightRank) = GetInfoShip(ship, droneLeft, droneRight);

        return new LevelExitDataBucketModel()
        {
            Stage = stage,
            Mode = mode.ToString(),
            Difficulty = difficulty,
            PlayIndex = playIndex,
            LoseIndex = loseIndex,
            ExitIndex = exitIndex,
            MsPlay = msDurationPlay,
            IsReplayAfterWin = isReplayAfterWin,
         
            ShipId = shipId,
            ShipLevel = shipLevel,
            
            DroneLeftId = droneLeftId,
            DroneLeftLevel = droneLeftLevel,
            DroneLeftRank = droneLeftRank,
            
            DroneRightId = droneRightId,
            DroneRightLevel = droneRightLevel,
            DroneRightRank = droneRightRank,
            
            InfoEquipment = infoEquipment,
            InfoGun = infoGun,
            WaveCleared = waveCleared,
            WaveTotal = waveTotal,
            LiveRemain = liveRemain,
        };
    }
    
    public static BaseDataBucketModel CreateLevelReOpen(int stage, int playIndex, int loseIndex,
        string isReplayAfterWin, PlayModeLevelDataBucket mode, string difficulty, string infoEquipment,
        EntityComponent? ship, EntityComponent? droneLeft, EntityComponent? droneRight)
    {
        var (shipId, shipLevel,
            droneLeftId, droneLeftLevel,
            droneLeftRank, droneRightId,
            droneRightLevel, droneRightRank) = GetInfoShip(ship, droneLeft, droneRight);

        return new LevelReOpenDataBucketModel()
        {
            Stage = stage,
            PlayIndex = playIndex,
            LoseIndex = loseIndex,
            IsReplayAfterWin = isReplayAfterWin,
         
            ShipId = shipId,
            ShipLevel = shipLevel,
            
            DroneLeftId = droneLeftId,
            DroneLeftLevel = droneLeftLevel,
            DroneLeftRank = droneLeftRank,
            
            DroneRightId = droneRightId,
            DroneRightLevel = droneRightLevel,
            DroneRightRank = droneRightRank,
            
            InfoEquipment = infoEquipment,
            Mode = mode.ToString(),
            Difficulty = difficulty,
        };
    }
    
    public static BaseDataBucketModel CreateResourceEarn(string name, string type, float amount, string extraDetail,
        string placement, string placementDetail, string reason)
    {
        return new ResourceEarnDataBucketModel()
        {
            ResourceName = name,
            ResourceType = type,
            ResourceAmount = amount.ToString(CultureInfo.InvariantCulture),
            ResourceDetail = extraDetail,
         
            Placement = placement,
            PlacementDetail = placementDetail,
            
            Reason = reason,
        };
    }
    
    public static BaseDataBucketModel CreateResourceSpend(string name, string type, float amount, string extraDetail,
        string placement, string placementDetail, string reason)
    {
        return new ResourceSpendDataBucketModel()
        {
            ResourceName = name,
            ResourceType = type,
            ResourceAmount = amount.ToString(CultureInfo.InvariantCulture),
            ResourceDetail = extraDetail,
         
            Placement = placement,
            PlacementDetail = placementDetail,
            
            Reason = reason,
        };
    }

    public static BaseDataBucketModel CreateIAPShow(string placementId, DataBucketConstant.IAPAnalytics.ShowType showType,
        DataBucketConstant.IAPAnalytics.TriggerType triggerType, string packName, string customPlacement = "")
    {
        return new IAPShowDataBucketModel()
        {
            Placement = placementId,
            ShowType = showType.ToString(),
            TriggerType = triggerType.ToString(),
            PackName = packName,
        };
    }
    
    public static BaseDataBucketModel CreateIAPClick(string placementId, DataBucketConstant.IAPAnalytics.ShowType showType,
        DataBucketConstant.IAPAnalytics.TriggerType triggerType, string packName, string customPlacement = "")
    {
        return new IAPClickDataBucketModel()
        {
            Placement = placementId,
            ShowType = showType.ToString(),
            TriggerType = triggerType.ToString(),
            PackName = packName,
        };
    }
    
    public static BaseDataBucketModel CreateIAPPurchase(string placementId, DataBucketConstant.IAPAnalytics.ShowType showType,
        DataBucketConstant.IAPAnalytics.TriggerType triggerType, string packName, float price, string currency, string customPlacement = "")
    {
        return new IAPPurchaseDataBucketModel()
        {
            Placement = placementId,
            ShowType = showType.ToString(),
            TriggerType = triggerType.ToString(),
            PackName = packName,
            Price = price,
            Currency = currency,
        };
    }
    
    public static BaseDataBucketModel CreateIAPClose(string placementId, DataBucketConstant.IAPAnalytics.ShowType showType,
        DataBucketConstant.IAPAnalytics.TriggerType triggerType, string packName, long duration)
    {
        return new IAPCloseDataBucketModel()
        {
            Placement = placementId,
            ShowType = showType.ToString(),
            TriggerType = triggerType.ToString(),
            PackName = packName,
            IAPduration = duration,
        };
    }

    public static BaseDataBucketModel CreateAdRequest(DataBucketConstant.AdsAnalytics.AdFormat adFormat, DataBucketConstant.AdsAnalytics.AdPlatform adPlatform, string adNetwork,
        string adUnitId, string placement, bool isLoad, long loadTime)
    {
        return new AdRequestDataBucketModel()
        {
            AdFormat = adFormat.ToString(),
            AdPlatform = adPlatform.ToString(),
            AdNetwork = adNetwork,
            AdUnitId = adUnitId,
            Placement = placement,
            IsLoad = isLoad ? 1 : 0,
            LoadTime = loadTime
        };
    }
    
    public static BaseDataBucketModel CreateAdImpression(DataBucketConstant.AdsAnalytics.AdFormat adFormat, DataBucketConstant.AdsAnalytics.AdPlatform adPlatform, string adNetwork,
        string adUnitId, string placement, bool isShow, float value)
    {
        return new AdImpressionDataBucketModel()
        {
            AdFormat = adFormat.ToString(),
            AdPlatform = adPlatform.ToString(),
            AdNetwork = adNetwork,
            AdUnitId = adUnitId,
            Placement = placement,
            IsShow = isShow ? 1 : 0,
            Value = value
        };
    }
    
    public static BaseDataBucketModel CreateAdClick(DataBucketConstant.AdsAnalytics.AdFormat adFormat, DataBucketConstant.AdsAnalytics.AdPlatform adPlatform, string adNetwork,
        string adUnitId, string placement)
    {
        return new AdClickDataBucketModel()
        {
            AdFormat = adFormat.ToString(),
            AdPlatform = adPlatform.ToString(),
            AdNetwork = adNetwork,
            AdUnitId = adUnitId,
            Placement = placement,
        };
    }
    
    public static BaseDataBucketModel CreateAdComplete(DataBucketConstant.AdsAnalytics.AdFormat adFormat, DataBucketConstant.AdsAnalytics.AdPlatform adPlatform, string adNetwork,
        string adUnitId, string placement, long adDuration, DataBucketConstant.AdsAnalytics.EndType endType)
    {
        return new AdCompleteDataBucketModel()
        {
            AdFormat = adFormat.ToString(),
            AdPlatform = adPlatform.ToString(),
            AdNetwork = adNetwork,
            AdUnitId = adUnitId,
            Placement = placement,
            EndType = endType.ToString(),
            AdDuration = adDuration,
        };
    }
    
    public static BaseDataBucketModel CreateTutorialAction(DataBucketConstant.Other.ActionCategory category, string actionName, int actionIndex)
    {
        return new TutorialActionDataBucketModel()
        {
            ActionCate = category.ToString(),
            ActionName = actionName,
            ActionIndex = actionIndex,
        };
    }

    public static Dictionary<string, object> ToDictionary(this object obj)
    {
        var dictionary = new Dictionary<string, object>();
        var type = obj.GetType();
        
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            var jsonAttribute = field.GetCustomAttribute<JsonPropertyAttribute>();
            string key = jsonAttribute != null ? jsonAttribute.PropertyName : field.Name;
            object value = field.GetValue(obj);

            if (key != null && value != null) dictionary.Add(key, value);
        }

        return dictionary;
    }

    private static (string, int?, string, int?, int?, string, int?, int?) GetInfoShip(EntityComponent? shipData,
        EntityComponent? droneLeft, EntityComponent? droneRight)
    {
        var shipId = shipData?.id;
        var shipLevel = shipData?.level;

        var droneLeftId = droneLeft?.id;
        var droneLeftLevel = droneLeft?.level;
        var droneLeftRank = (int?)droneLeft?.grade;

        var droneRightId = droneRight?.id;
        var droneRightLevel = droneRight?.level;
        var droneRightRank = (int?)droneRight?.grade;

        return (shipId, shipLevel, droneLeftId, droneLeftLevel, droneLeftRank, droneRightId, droneRightLevel, droneRightRank);
    }
}
