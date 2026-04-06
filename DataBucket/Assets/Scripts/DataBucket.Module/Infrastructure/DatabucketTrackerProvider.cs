using System;
using System.Collections.Generic;
using Databuckets;

public class DatabucketTrackerProvider : IDatabucketTrackerProvider
{
    public void Init(string endpoint, string apiKey)
    {
        DatabucketsTracker.Init(endpoint, apiKey);
    }

    public void Record(string eventName, Dictionary<string, object> parameters)
    {
        DatabucketsTracker.Record(eventName, parameters);
    }

    public void SetCommonProperty(string key, object value)
    {
        DatabucketsTracker.SetCommonProperty(key, value);
    }

    public void SetCommonProperties(Dictionary<string, object> parameters)
    {
        DatabucketsTracker.SetCommonProperties(parameters);
    }

    public void ForceEndCurrentSession()
    {
        DatabucketsTracker.ForceEndCurrentSession();
    }
}
