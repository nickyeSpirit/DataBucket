using JetBrains.Annotations;
using Newtonsoft.Json;

public class TutorialActionDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("action_cate")] [CanBeNull] 
    public string ActionCate;

    [JsonProperty("action_name")] [CanBeNull] 
    public string ActionName;
    
    [JsonProperty("action_index")]
    public int? ActionIndex;
}

public class ButtonClickDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("button_name")] [CanBeNull] 
    public string ButtonName;

    [JsonProperty("screen_name")] [CanBeNull] 
    public string ScreenName;
}

public class ScreenGoDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("screen_name")] [CanBeNull] 
    public string ActionCate;

    [JsonProperty("button_name")] [CanBeNull] 
    public string ActionName;
    
    [JsonProperty("screen_prev_name")] [CanBeNull] 
    public string ScreenPrevName;
    
    [JsonProperty("duration_screen_prev")]
    public long? DurationScreenPrev;
}

public class ScreenExitDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("screen_prev_name")] [CanBeNull] 
    public string ScreenPrevName;
    
    [JsonProperty("duration_screen_prev")]
    public long? DurationScreenPrev;
}