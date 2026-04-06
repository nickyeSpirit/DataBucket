using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class UnityWebCountryCodeProvider : ICountryCodeProvider
{
    [Serializable]
    public class IpResponse
    {
        public string ip;
        public string country;
    }

    public void GetCountryCodeAsync(Action<string> onResult)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            onResult?.Invoke(GetOfflineRegion());
            return;
        }

        _ = TaskGetCountry(onResult);
    }

    private async Task TaskGetCountry(Action<string> onComplete)
    {
        string url = "https://api.country.is/";
        string countryCode = null;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.timeout = 5;

            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            try
            {
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("GetCountryCode Error: " + request.error);
                }
                else
                {
                    string jsonResult = request.downloadHandler.text;
                    IpResponse info = JsonUtility.FromJson<IpResponse>(jsonResult);
                    if (info != null && !string.IsNullOrEmpty(info.country))
                    {
                        countryCode = info.country;
                    }

                    Debug.Log($"[Country] > {countryCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("GetCountryCode Exception: " + ex.Message);
            }
        }

        if (string.IsNullOrEmpty(countryCode))
        {
            countryCode = GetOfflineRegion();
        }

        onComplete?.Invoke(countryCode);
    }

    private string GetOfflineRegion()
    {
        try
        {
            return RegionInfo.CurrentRegion.TwoLetterISORegionName;
        }
        catch
        {
            return "US";
        }
    }
}
