using System.Collections.Generic;

public interface IDatabucketTrackerProvider
{
    void Init(string endpoint, string apiKey);
    void Record(string eventName, Dictionary<string, object> parameters);
    void SetCommonProperty(string key, object value);
    void SetCommonProperties(Dictionary<string, object> parameters);
    void ForceEndCurrentSession();
}
