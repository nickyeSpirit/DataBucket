using System;

public class AttributionData
{
    public string Network { get; set; }
    public string Campaign { get; set; }
    public string Adgroup { get; set; }
    public string Creative { get; set; }
    public string TrackerName { get; set; }
}

public interface IDataBucketAttributionProvider
{
    void FetchAttribution(Action<AttributionData> onAttributionFetched);
}
