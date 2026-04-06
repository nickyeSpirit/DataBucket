using System.Collections.Generic;

public interface IDataBucketExperimentProvider
{
    /// <summary>
    /// Returns a list of active experiments in the format 'key:variant'
    /// </summary>
    List<string> GetActiveExperiments();
}
