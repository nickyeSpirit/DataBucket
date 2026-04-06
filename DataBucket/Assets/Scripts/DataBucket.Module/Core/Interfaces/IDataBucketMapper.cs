using System;
using JetBrains.Annotations;
using static DataBucketExtensions;

[Serializable]
public struct PropertiesDataBucketIn
{
    public string UserId;
    public int StageCurrent;
    public int StageMax;
    public string PlayerName;
    public int PlayerLevel;
}

[Serializable]
public struct LevelPlayDataBucketIn
{
    public EntityComponent? Ship;
    public EntityComponent? DroneLeft;
    public EntityComponent? DroneRight;
    public EntityComponent? Gun;
    
    public DifficultyModeLevelDataBucket Difficulty;
}

[Serializable]
public struct GamePlayDataBucket
{
    public int? WaveCleared;
    public int? WaveTotal;
    public float? LiveRemain;
    public bool? IsWin;
    public int? Score;
    public bool? DidQuitByUser;
}

public interface IDataBucketMapper
{
    PropertiesDataBucketIn PropertiesDataBucketIn { get; }
    LevelPlayDataBucketIn LevelPlayDataBucketIn { get; }
    PlayModeLevelDataBucket Mode { get; }
    GamePlayDataBucket GamePlay { get; }
    int CurrentMapSelect { get; }
    int TotalMapStoryMode { get; }
    int TotalTutorial { get; }
    int TotalMapBossRaid { get; }
    int TotalMapEndless { get; }
}

public interface IEntityComponent
{
    [CanBeNull] string id { get; set; }
    int? level { get; set; }
    int? grade { get; set; }
}

public struct EntityComponent : IEntityComponent
{
    [CanBeNull] public string id { get; set; }
    public int? level { get; set; }
    public int? grade { get; set; }
}

public enum PlayModeLevelDataBucket
{
    Endless = 0, Tutorial = 1, StoryMode = 5, BossRaid = 8
}

public enum DifficultyModeLevelDataBucket
{
    Easy, Normal, Hard
}