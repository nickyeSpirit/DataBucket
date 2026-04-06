using AdjustSdk;
using System;

public class AdjustAttributionProvider : IDataBucketAttributionProvider
{
    public void FetchAttribution(Action<AttributionData> onAttributionFetched)
    {
        Adjust.GetAttribution((adjustAttribution) =>
        {
            if (adjustAttribution != null)
            {
                var data = new AttributionData
                {
                    Network = adjustAttribution.Network,
                    Campaign = adjustAttribution.Campaign,
                    Adgroup = adjustAttribution.Adgroup,
                    Creative = adjustAttribution.Creative,
                    TrackerName = adjustAttribution.TrackerName
                };
                onAttributionFetched?.Invoke(data);
            }
            else
            {
                onAttributionFetched?.Invoke(null);
            }
        });
    }
}
