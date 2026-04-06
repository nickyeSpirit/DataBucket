using UnityEngine;

[CreateAssetMenu(fileName = "DataBucketConfig", menuName = "Setting/DataBucket Config", order = 1)]
public class DataBucketConfigSO : ScriptableObject, IDataBucketConfig
{
    [SerializeField] private string _apiEndpoint = "";
    [SerializeField] private string _apiKey = "";
    [SerializeField] private float _initDelaySeconds = 2.0f;

    public string ApiEndpoint => _apiEndpoint;
    public string ApiKey => _apiKey;
    public float InitDelaySeconds => _initDelaySeconds;
}
