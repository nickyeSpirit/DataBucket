using JetBrains.Annotations;
using Newtonsoft.Json;


public class AdRequestDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("ad_format")] [CanBeNull] 
    public string AdFormat;

    [JsonProperty("ad_platform")] [CanBeNull] 
    public string AdPlatform;
    
    [JsonProperty("ad_network")] [CanBeNull] 
    public string AdNetwork;
    
    [JsonProperty("ad_unit_id")] [CanBeNull] 
    public string AdUnitId;
    
    [JsonProperty("placement")] [CanBeNull] 
    public string Placement;
    
    [JsonProperty("is_load")]
    public int? IsLoad;
    
    [JsonProperty("load_time")]
    public long? LoadTime;
}

public class AdImpressionDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("ad_format")] [CanBeNull] 
    public string AdFormat;

    [JsonProperty("ad_platform")] [CanBeNull] 
    public string AdPlatform;
    
    [JsonProperty("ad_network")] [CanBeNull] 
    public string AdNetwork;
    
    [JsonProperty("ad_unit_id")] [CanBeNull] 
    public string AdUnitId;
    
    [JsonProperty("placement")] [CanBeNull] 
    public string Placement;
    
    [JsonProperty("is_show")]
    public int? IsShow;
    
    [JsonProperty("value")]
    public float? Value;
}

public class AdClickDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("ad_format")] [CanBeNull] 
    public string AdFormat;

    [JsonProperty("ad_platform")] [CanBeNull] 
    public string AdPlatform;
    
    [JsonProperty("ad_network")] [CanBeNull] 
    public string AdNetwork;
    
    [JsonProperty("ad_unit_id")] [CanBeNull] 
    public string AdUnitId;
    
    [JsonProperty("placement")] [CanBeNull] 
    public string Placement;
}

public class AdCompleteDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("ad_format")] [CanBeNull] 
    public string AdFormat;

    [JsonProperty("ad_platform")] [CanBeNull] 
    public string AdPlatform;
    
    [JsonProperty("ad_network")] [CanBeNull] 
    public string AdNetwork;
    
    [JsonProperty("ad_unit_id")] [CanBeNull] 
    public string AdUnitId;
    
    [JsonProperty("end_type")] [CanBeNull] 
    public string EndType;
    
    [JsonProperty("ad_duration")]
    public long? AdDuration;
    
    [JsonProperty("placement")] [CanBeNull] 
    public string Placement;
}