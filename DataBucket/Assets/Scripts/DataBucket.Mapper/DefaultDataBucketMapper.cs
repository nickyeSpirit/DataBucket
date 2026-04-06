using System;
using System.Collections;

public class DefaultDataBucketMapper : IDataBucketMapper
{
    public static DefaultDataBucketMapper Instance
    {
        get
        {
            _instance ??= new DefaultDataBucketMapper();
            return _instance;
        }
    }

    private static DefaultDataBucketMapper _instance;

    public PropertiesDataBucketIn PropertiesDataBucketIn
    {
        get
        {
            var storyMode = DataManager.Instance.csSoryModeData;
            var playerData = DataManager.Instance.playerData;
            var stageCurrent = 0;
            var userId = string.Empty;
            var stageMax = 0;
            var playerName = string.Empty;
            var playerLevel = 0;

            try
            {
                stageCurrent = storyMode.currentMapSelected;
                userId = playerData.userId;
                stageMax = storyMode.currentMap;
                playerName = playerData.name;
                playerLevel = playerData.level;
            }
            catch (Exception e)
            {
            }

            var propertiesDataBucketIn = new PropertiesDataBucketIn()
            {
                StageMax = stageMax,
                StageCurrent = stageCurrent,
                PlayerName = playerName,
                PlayerLevel = playerLevel,
                UserId = userId,
            };

            return propertiesDataBucketIn;
        }
    }

    public LevelPlayDataBucketIn LevelPlayDataBucketIn
    {
        get
        {
            var (shipComponent, droneLeftComponent, droneRightComponent, gunComponent, difficulty) = GetLevelPlayInfo();
            var levelPlayDataBucketIn = new LevelPlayDataBucketIn()
            {
                Ship = shipComponent,
                DroneLeft = droneLeftComponent,
                DroneRight = droneRightComponent,
                Gun = gunComponent,
                Difficulty = difficulty,
            };

            return levelPlayDataBucketIn;
        }
    }

    public PlayModeLevelDataBucket Mode => GetPlayModeLevelDataBucket(GameSetting.gamePlayMode);

    public GamePlayDataBucket GamePlay
    {
        get
        {
            var gamePlayManager = GamePlayManager.Instance;
            return new GamePlayDataBucket()
            {
                Score = gamePlayManager?.collectionInfo?.score,
                IsWin = gamePlayManager?.collectionInfo?.isWin,
                WaveCleared = gamePlayManager?.waveManager.indexWave,
                WaveTotal = gamePlayManager?.currentGamePlayInfo?.GetTotalWaveThisStage(),
                LiveRemain = gamePlayManager?.playerManager?.mountController?.hp,
                DidQuitByUser= gamePlayManager?.didQuitByUser,
            };
        }
    }

    public int CurrentMapSelect
    {
        get
        {
            var mode = Mode;
            if (mode == PlayModeLevelDataBucket.StoryMode)
            {
                return DataManager.Instance.csSoryModeData.currentMapSelected;
            }
            else if (mode == PlayModeLevelDataBucket.BossRaid)
            {
                return 0;
            }
            else if (mode == PlayModeLevelDataBucket.Endless)
            {
                return 0;
            }
            else if (mode == PlayModeLevelDataBucket.Tutorial)
            {
                return 0;
            }

            return DataManager.Instance.csSoryModeData.currentMapSelected;
        }
    }

    public int TotalMapStoryMode => DataManager.Instance.csSoryModeData.listMapStoryMapDetail;
    public int TotalTutorial => 1;
    public int TotalMapBossRaid => 1;
    public int TotalMapEndless => 1;

    private (EntityComponent?, EntityComponent?, EntityComponent?, EntityComponent?, DifficultyModeLevelDataBucket)
        GetLevelPlayInfo()
    {
        var shipData = DataManager.Instance.inventoryData.GetCurrentShipData();
        var gun = DataManager.Instance.inventoryData.gun;
        var droneLeft = DataManager.Instance.inventoryData.droneLeft;
        var droneRight = DataManager.Instance.inventoryData.droneRight;
        var difficulty = DataManager.Instance.csSoryModeData.difficultySelected;

        EntityComponent? shipComponent = null;
        if (shipData != null)
        {
            shipComponent = new EntityComponent()
            {
                id = shipData?.id.ToString(),
                level = shipData?.level,
            };
        }

        EntityComponent? droneLeftComponent = null;
        if (droneLeft != null)
        {
            droneLeftComponent = new EntityComponent()
            {
                id = droneLeft.id.ToString(),
                level = droneLeft.level,
                grade = (int)droneLeft.grade,
            };
        }

        EntityComponent? droneRightComponent = null;
        if (droneRight != null)
        {
            droneRightComponent = new EntityComponent()
            {
                id = droneRight.id.ToString(),
                level = droneRight.level,
                grade = (int)droneRight.grade,
            };
        }

        EntityComponent? gunComponent = null;
        if (gun != null)
        {
            gunComponent = new EntityComponent()
            {
                id = gun.id.ToString(),
                level = gun.level,
                grade = (int)gun.grade,
            };
        }

        return (shipComponent, droneLeftComponent, droneRightComponent, gunComponent,
            (DifficultyModeLevelDataBucket)difficulty);
    }

    private PlayModeLevelDataBucket GetPlayModeLevelDataBucket(GameSetting.GamePlayMode mode)
    {
        var playMode = (PlayModeLevelDataBucket)mode;

        return playMode;
    }
}