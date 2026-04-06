using System;

public interface ICountryCodeProvider
{
    /// <summary>
    /// Gets the 2-letter country code (e.g. "US"). 
    /// </summary>
    void GetCountryCodeAsync(Action<string> onResult);
}
