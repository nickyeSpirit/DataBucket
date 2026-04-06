using JetBrains.Annotations;
using Newtonsoft.Json;

public class IAPShowDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("placement")] [CanBeNull] 
    public string Placement;

    [JsonProperty("show_type")] [CanBeNull] 
    public string ShowType;
    
    [JsonProperty("trigger_type")] [CanBeNull] 
    public string TriggerType;
    
    [JsonProperty("pack_name")] [CanBeNull] 
    public string PackName;
}

public class IAPClickDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("placement")] [CanBeNull] 
    public string Placement;

    [JsonProperty("show_type")] [CanBeNull] 
    public string ShowType;
    
    [JsonProperty("trigger_type")] [CanBeNull] 
    public string TriggerType;
    
    [JsonProperty("pack_name")] [CanBeNull] 
    public string PackName;
}

public class IAPPurchaseDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("placement")] [CanBeNull] 
    public string Placement;

    [JsonProperty("show_type")] [CanBeNull] 
    public string ShowType;
    
    [JsonProperty("trigger_type")] [CanBeNull] 
    public string TriggerType;
    
    [JsonProperty("pack_name")] [CanBeNull] 
    public string PackName;
    
    [JsonProperty("price")]
    public float? Price;
    
    [JsonProperty("currency")] [CanBeNull] 
    public string Currency;
}

public class IAPCloseDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("placement")] [CanBeNull] 
    public string Placement;

    [JsonProperty("show_type")] [CanBeNull] 
    public string ShowType;
    
    [JsonProperty("trigger_type")] [CanBeNull] 
    public string TriggerType;
    
    [JsonProperty("pack_name")] [CanBeNull] 
    public string PackName;
    
    [JsonProperty("iap_duration")]
    public long? IAPduration;
}