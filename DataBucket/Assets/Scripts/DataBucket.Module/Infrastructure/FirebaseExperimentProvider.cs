//using Firebase.RemoteConfig;
using System.Collections.Generic;

public class FirebaseExperimentProvider : IDataBucketExperimentProvider
{
    private static readonly string[] ExperimentKeys = { /* "kLDVersion" */ };

    public List<string> GetActiveExperiments()
    {
        var experiments = new List<string>();
        foreach (string key in ExperimentKeys)
        {
            string rawString = "";//""irebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue;
            if (!string.IsNullOrEmpty(rawString))
                experiments.Add($"{key}:{rawString}");
        }
        return experiments;
    }
}
