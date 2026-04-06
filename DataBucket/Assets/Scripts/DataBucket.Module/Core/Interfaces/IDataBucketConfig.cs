public interface IDataBucketConfig
{
    string ApiEndpoint { get; }
    string ApiKey { get; }
    float InitDelaySeconds { get; }
}
