using System;

public interface IDataBucketLogger
{
    void Log(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogException(Exception exception);
}
