public class MaxAdInfo : IAdInfo
{
    public string NetworkName { get; }

    public DataBucketConstant.AdsAnalytics.AdPlatform AdPlatForm { get; } =
        DataBucketConstant.AdsAnalytics.AdPlatform.Max;

    public string AdUnitId { get; }
    public float Revenue { get; }

    public MaxAdInfo(string networkName, string adUnitId, float revenue = 0)
    {
        NetworkName = networkName;
        AdUnitId = adUnitId;
        Revenue = revenue;
    }

    public MaxAdInfo()
    {
        NetworkName = "null";
        AdUnitId = "null";
        Revenue = 0;
    }
}