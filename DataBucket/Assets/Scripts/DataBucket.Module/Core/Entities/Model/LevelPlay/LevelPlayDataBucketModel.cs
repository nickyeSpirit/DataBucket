using JetBrains.Annotations;
using Newtonsoft.Json;

public class LevelStartDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("stage")]
    public int? Stage;

    [JsonProperty("chapter")]
    public int? Chapter;
    
    [JsonProperty("play_index")]
    public int? PlayIndex;
    
    [JsonProperty("lose_index")]
    public int? LoseIndex;
    
    [JsonProperty("is_replay_after_win")] [CanBeNull]
    public string IsReplayAfterWin;
    
    [JsonProperty("duration_stage_load")]
    public long? MsLoad;
    
    [JsonProperty("mode")] [CanBeNull] 
    public string Mode;
    
    [JsonProperty("difficulty")] [CanBeNull] 
    public string Difficulty;

    [JsonProperty("ship_id")] [CanBeNull] 
    public string ShipId;
    
    [JsonProperty("ship_level")]
    public int? ShipLevel;
    
    [JsonProperty("ship_rank")]
    public int? ShipRank;
    
    [JsonProperty("ship_element")]
    public int? ShipElement;
    
    [JsonProperty("info_equipment")] [CanBeNull] 
    public string InfoEquipment;

    [JsonProperty("drone_left_id")] [CanBeNull] 
    public string DroneLeftId;
    
    [JsonProperty("drone_left_level")]
    public int? DroneLeftLevel;
    
    [JsonProperty("drone_left_rank")]
    public int? DroneLeftRank;
    
    [JsonProperty("drone_left_element")]
    public int? DroneLeftElement;
    
    [JsonProperty("drone_right_id")] [CanBeNull] 
    public string DroneRightId;
    
    [JsonProperty("drone_right_level")]
    public int? DroneRightLevel;
    
    [JsonProperty("drone_right_rank")]
    public int? DroneRightRank;
    
    [JsonProperty("drone_right_element")]
    public int? DroneRightElement;
    
    [JsonProperty("wave_total")]
    public int? WaveTotal;
}

public class LevelEnterDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("stage")]
    public int? Stage;

    [JsonProperty("chapter")]
    public int? Chapter;
    
    [JsonProperty("play_index")]
    public int? PlayIndex;
    
    [JsonProperty("lose_index")]
    public int? LoseIndex;
    
    [JsonProperty("is_replay_after_win")] [CanBeNull]
    public string IsReplayAfterWin;
    
    [JsonProperty("mode")] [CanBeNull] 
    public string Mode;

    [JsonProperty("difficulty")] [CanBeNull] 
    public string Difficulty;
    
    [JsonProperty("ship_id")] [CanBeNull] 
    public string ShipId;
    
    [JsonProperty("ship_level")]
    public int? ShipLevel;
    
    [JsonProperty("ship_rank")]
    public int? ShipRank;
    
    [JsonProperty("ship_element")]
    public int? ShipElement;
    
    [JsonProperty("info_equipment")] [CanBeNull] 
    public string InfoEquipment;

    [JsonProperty("drone_left_id")] [CanBeNull] 
    public string DroneLeftId;
    
    [JsonProperty("drone_left_level")]
    public int? DroneLeftLevel;
    
    [JsonProperty("drone_left_rank")]
    public int? DroneLeftRank;
    
    [JsonProperty("drone_left_element")]
    public int? DroneLeftElement;
    
    [JsonProperty("drone_right_id")] [CanBeNull] 
    public string DroneRightId;
    
    [JsonProperty("drone_right_level")]
    public int? DroneRightLevel;
    
    [JsonProperty("drone_right_rank")]
    public int? DroneRightRank;
    
    [JsonProperty("drone_right_element")]
    public int? DroneRightElement;
}

public class LevelEndDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("stage")]
    public int? Stage;

    [JsonProperty("chapter")]
    public int? Chapter;
    
    [JsonProperty("mode")] [CanBeNull] 
    public string Mode;
    
    [JsonProperty("difficulty")] [CanBeNull] 
    public string Difficulty;
    
    [JsonProperty("play_index")]
    public int? PlayIndex;
    
    [JsonProperty("lose_index")]
    public int? LoseIndex;
    
    [JsonProperty("duration_play")]
    public long? MsPlay;
    
    [JsonProperty("is_replay_after_win")] [CanBeNull]
    public string IsReplayAfterWin;
    
    [JsonProperty("ship_id")] [CanBeNull] 
    public string ShipId;
    
    [JsonProperty("ship_level")]
    public int? ShipLevel;
    
    [JsonProperty("ship_rank")]
    public int? ShipRank;
    
    [JsonProperty("ship_element")]
    public int? ShipElement;
    
    [JsonProperty("drone_left_id")] [CanBeNull] 
    public string DroneLeftId;
    
    [JsonProperty("drone_left_level")]
    public int? DroneLeftLevel;
    
    [JsonProperty("drone_left_rank")]
    public int? DroneLeftRank;
    
    [JsonProperty("drone_left_element")]
    public int? DroneLeftElement;
    
    [JsonProperty("drone_right_id")] [CanBeNull] 
    public string DroneRightId;
    
    [JsonProperty("drone_right_level")]
    public int? DroneRightLevel;
    
    [JsonProperty("drone_right_rank")]
    public int? DroneRightRank;
    
    [JsonProperty("drone_right_element")]
    public int? DroneRightElement;
    
    [JsonProperty("info_equipment")] [CanBeNull] 
    public string InfoEquipment;
    
    [JsonProperty("info_gun")] [CanBeNull] 
    public string InfoGun;
    
    [JsonProperty("wave_cleared")]
    public int? WaveCleared;
    
    [JsonProperty("wave_total")]
    public int? WaveTotal;
    
    [JsonProperty("live_remain")]
    public int? LiveRemain;
   
    [JsonProperty("result")] [CanBeNull] 
    public string Result;
    
    [JsonProperty("score")]
    public int? Score;
    
    [JsonProperty("lose_by")] [CanBeNull] 
    public string LoseBy;
   
}

public class LevelExitDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("stage")]
    public int? Stage;

    [JsonProperty("chapter")]
    public int? Chapter;
    
    [JsonProperty("mode")] [CanBeNull] 
    public string Mode;
    
    [JsonProperty("difficulty")] [CanBeNull] 
    public string Difficulty;
    
    [JsonProperty("play_index")]
    public int? PlayIndex;
    
    [JsonProperty("lose_index")]
    public int? LoseIndex;
    
    [JsonProperty("exit_index")]
    public int? ExitIndex;
    
    [JsonProperty("duration_play")]
    public long? MsPlay;
    
    [JsonProperty("is_replay_after_win")] [CanBeNull]
    public string IsReplayAfterWin;
    
    [JsonProperty("ship_id")] [CanBeNull] 
    public string ShipId;
    
    [JsonProperty("ship_level")]
    public int? ShipLevel;
    
    [JsonProperty("ship_rank")]
    public int? ShipRank;
    
    [JsonProperty("ship_element")]
    public int? ShipElement;
    
    [JsonProperty("drone_left_id")] [CanBeNull] 
    public string DroneLeftId;
    
    [JsonProperty("drone_left_level")]
    public int? DroneLeftLevel;
    
    [JsonProperty("drone_left_rank")]
    public int? DroneLeftRank;
    
    [JsonProperty("drone_left_element")]
    public int? DroneLeftElement;
    
    [JsonProperty("drone_right_id")] [CanBeNull] 
    public string DroneRightId;
    
    [JsonProperty("drone_right_level")]
    public int? DroneRightLevel;
    
    [JsonProperty("drone_right_rank")]
    public int? DroneRightRank;
    
    [JsonProperty("drone_right_element")]
    public int? DroneRightElement;
    
    [JsonProperty("info_equipment")] [CanBeNull] 
    public string InfoEquipment;
    
    [JsonProperty("info_gun")] [CanBeNull] 
    public string InfoGun;
    
    [JsonProperty("wave_cleared")]
    public int? WaveCleared;
    
    [JsonProperty("wave_total")]
    public int? WaveTotal;
    
    [JsonProperty("live_remain")]
    public int? LiveRemain;
   
    [JsonProperty("result")] [CanBeNull] 
    public string Result;
    
    [JsonProperty("score")]
    public int? Score;
    
    [JsonProperty("lose_by")] [CanBeNull] 
    public string LoseBy;
}

public class LevelReOpenDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("stage")]
    public int? Stage;

    [JsonProperty("chapter")]
    public int? Chapter;
    
    [JsonProperty("mode")] [CanBeNull] 
    public string Mode;
    
    [JsonProperty("difficulty")] [CanBeNull] 
    public string Difficulty;
    
    [JsonProperty("play_index")]
    public int? PlayIndex;
    
    [JsonProperty("lose_index")]
    public int? LoseIndex;
    
    [JsonProperty("exit_index")]
    public int? ExitIndex;
    
    [JsonProperty("duration_play")]
    public long? MsPlay;
    
    [JsonProperty("is_replay_after_win")] [CanBeNull]
    public string IsReplayAfterWin;
    
    [JsonProperty("ship_id")] [CanBeNull] 
    public string ShipId;
    
    [JsonProperty("ship_level")]
    public int? ShipLevel;
    
    [JsonProperty("ship_rank")]
    public int? ShipRank;
    
    [JsonProperty("ship_element")]
    public int? ShipElement;
    
    [JsonProperty("drone_left_id")] [CanBeNull] 
    public string DroneLeftId;
    
    [JsonProperty("drone_left_level")]
    public int? DroneLeftLevel;
    
    [JsonProperty("drone_left_rank")]
    public int? DroneLeftRank;
    
    [JsonProperty("drone_left_element")]
    public int? DroneLeftElement;
    
    [JsonProperty("drone_right_id")] [CanBeNull] 
    public string DroneRightId;
    
    [JsonProperty("drone_right_level")]
    public int? DroneRightLevel;
    
    [JsonProperty("drone_right_rank")]
    public int? DroneRightRank;
    
    [JsonProperty("drone_right_element")]
    public int? DroneRightElement;
    
    [JsonProperty("info_equipment")] [CanBeNull] 
    public string InfoEquipment;
    
    [JsonProperty("info_gun")] [CanBeNull] 
    public string InfoGun;
    
    [JsonProperty("wave_cleared")]
    public int? WaveCleared;
    
    [JsonProperty("wave_total")]
    public int? WaveTotal;
    
    [JsonProperty("live_remain")]
    public int? LiveRemain;
   
    [JsonProperty("result")] [CanBeNull] 
    public string Result;
    
    [JsonProperty("score")]
    public int? Score;
    
    [JsonProperty("lose_by")] [CanBeNull] 
    public string LoseBy;
}