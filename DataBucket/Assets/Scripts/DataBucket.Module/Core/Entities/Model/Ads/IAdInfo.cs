public interface IAdInfo
{
    string NetworkName { get; }
    DataBucketConstant.AdsAnalytics.AdPlatform AdPlatForm { get; }
    string AdUnitId { get; }
    float Revenue { get; }
}

