using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

[Serializable]
public class DataBucketTracker
{
    //User play index per stage
    public List<int> playIndex;
    
    //User loses per stage (before starting current play)
    public List<int> loseIndex;
    
    /// <summary>Total stars of played maps</summary>
    public List<int> stars;
    public List<int> levelEnterCounter;
    
    public int ExitIndex { get; set; }

    public int GetPlayIndexAt(int mapIdx)
    {
        if (mapIdx < playIndex.Count)
        {
            return playIndex[mapIdx];
        }

        return 0;
    }

    public int GetLoseIndexAt(int mapIdx)
    {
        if (mapIdx < loseIndex.Count)
        {
            return loseIndex[mapIdx];
        }

        return 0;
    }

    public bool IsPassedMap(int mapIndex)
    {
        bool result = false;

        if (mapIndex >= 0 && mapIndex < stars.Count)
        {
            return stars[mapIndex] > 0;
        }

        return result;
    }
    
    public void EnterLevel(int level)
    {
        levelEnterCounter[level]++;

        if (level < playIndex.Count)
        {
            playIndex[level]++;
        }
    }

    public void LoseLevel(int level)
    {
        if (level < loseIndex.Count)
        {
            loseIndex[level]++;
        }
    }
    
    public void ValidateLevelEnterCounter(int totalMap)
    {
        if (levelEnterCounter == null)
        {
            levelEnterCounter = new List<int>();
        }

        if (playIndex == null)
        {
            playIndex = new List<int>();
        }

        if (loseIndex == null)
        {
            loseIndex = new List<int>();
        }

        if (stars == null)
        {
            stars = new List<int>();
        }

        if (stars.Count != totalMap)
        {
            for (int i = stars.Count; i < totalMap; i++)
            {
                stars.Add(0);
            }
        }

        if (playIndex.Count != totalMap)
        {
            for (int i = playIndex.Count; i < totalMap; i++)
            {
                playIndex.Add(0);
            }
        }

        if (loseIndex.Count != totalMap)
        {
            for (int i = loseIndex.Count; i < totalMap; i++)
            {
                loseIndex.Add(0);
            }
        }
        
        if (levelEnterCounter.Count != totalMap)
        {
            for (int i = levelEnterCounter.Count; i < totalMap; i++)
            {
                levelEnterCounter.Add(0);
            }
        }
    }
}

[Serializable]
public class DataBucketRuntime
{
    #region Serialize

    public DataBucketTracker tutorialTracker;
    public DataBucketTracker storyModeTracker;
    public DataBucketTracker endlessTracker;
    public DataBucketTracker bossRaidTracker;
    
    public string databucketUUID;
    public int dayActiveCount;

    #endregion

    #region NonSerialize

    public Stopwatch durationStageLoad { get; set; } = new Stopwatch();
    public Stopwatch durationStagePlay { get; set; } = new Stopwatch();
    public Stopwatch durationStayIAP { get; set; } = new Stopwatch();
    public Stopwatch durationAdRequest { get; set; } = new Stopwatch();
    public Stopwatch durationAdComplete { get; set; } = new Stopwatch();
    public int dayActiveCountForDataBucket
    {
        get
        {
            int _value = dayActiveCount;
            if (_value <= 0)
                _value = 0;
            return _value;
        }
    }
    
    #endregion
    
    public DataBucketTracker currentModeTracker
    {
        get
        {
            var mode = DataBucketController.Instance.DataBucketMapper.Mode;
            if (mode == PlayModeLevelDataBucket.StoryMode)
            {
                return storyModeTracker;
            }
            else if (mode == PlayModeLevelDataBucket.BossRaid)
            {
                return bossRaidTracker;
            }
            else if (mode == PlayModeLevelDataBucket.Endless)
            {
                return endlessTracker;
            }
            else if (mode == PlayModeLevelDataBucket.Tutorial)
            {
                return tutorialTracker;
            }

            return storyModeTracker;
        }
    }

    public string LogGunInfo(EntityComponent? gun)
    {
        if (gun == null)
        {
            return null;
        }

        string result = "";
        result += $"{gun.Value.id}_{gun.Value.level}_{gun.Value.grade},";

        if (result.EndsWith(","))
            result = result.Substring(0, result.Length - 1); // remove last character

        if (string.IsNullOrEmpty(result))
        {
            return null;
        }

        return result;
    }
    
    public string LogEquipmentsInfo(EntityComponent? droneLeft, EntityComponent? droneRight)
    {
        string result = "";

        if (droneLeft != null)
        {
            result += $"{droneLeft.Value.id}_{droneLeft.Value.level}_{droneLeft.Value.grade},";
        }

        if (droneRight != null)
        {
            result += $"{droneRight.Value.id}_{droneRight.Value.level}_{droneRight.Value.grade},";
        }

        if (result.EndsWith(","))
            result = result.Substring(0, result.Length - 1); // remove last character

        if (string.IsNullOrEmpty(result))
        {
            return null;
        }

        return result;
    }

    public void Init()
    {
        
    }

    public void ValidateData(
        int totalStory = 100, 
        int totalBoss = 1, 
        int totalEndless = 1, 
        int totalTutorial = 1, 
        IDataBucketLogger logger = null)
    {
        storyModeTracker ??= new DataBucketTracker();
        bossRaidTracker ??= new DataBucketTracker();
        endlessTracker ??= new DataBucketTracker();
        tutorialTracker ??= new DataBucketTracker();
        
        bossRaidTracker.ValidateLevelEnterCounter(totalBoss);
        endlessTracker.ValidateLevelEnterCounter(totalEndless);
        storyModeTracker.ValidateLevelEnterCounter(totalStory);
        tutorialTracker.ValidateLevelEnterCounter(totalTutorial);

        if (string.IsNullOrEmpty(databucketUUID))
        {
            databucketUUID = GenerateDatabucketUUID(logger);
        }
    }
    
    private string GenerateDatabucketUUID(IDataBucketLogger logger)
    {
        var rawId = $"{System.Guid.NewGuid()}_{System.DateTime.UtcNow.Ticks}";
        var uuid = CalculateMD5(rawId);
        
        if (logger != null)
            logger.Log($"[Databuckets] GenerateDatabucketUUID: {uuid}");

        return uuid;
    }
    
    public static string CalculateMD5(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
