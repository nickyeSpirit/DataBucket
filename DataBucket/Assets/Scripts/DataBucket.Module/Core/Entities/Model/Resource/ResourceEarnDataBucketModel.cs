using JetBrains.Annotations;
using Newtonsoft.Json;

public class ResourceEarnDataBucketModel : BaseDataBucketModel
{
    [JsonProperty("resource_type")] [CanBeNull] 
    public string ResourceType;

    [JsonProperty("resource_name")] [CanBeNull] 
    public string ResourceName;
    
    [JsonProperty("resource_amount")] [CanBeNull] 
    public string ResourceAmount;
    
    [JsonProperty("resource_detail")] [CanBeNull] 
    public string ResourceDetail;
    
    [JsonProperty("placement")] [CanBeNull] 
    public string Placement;
    
    [JsonProperty("placement_detail")] [CanBeNull] 
    public string PlacementDetail;
    
    [JsonProperty("reason")] [CanBeNull] 
    public string Reason;
}