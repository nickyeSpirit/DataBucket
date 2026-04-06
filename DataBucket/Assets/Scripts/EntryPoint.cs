using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public void Awake()
    {
        var config = Resources.Load<DataBucketConfigSO>("DataBucketConfig");
        if (config == null) 
        {
            config = ScriptableObject.CreateInstance<DataBucketConfigSO>();
        }

        var mapper = DefaultDataBucketMapper.Instance;
        DataBucketController.Instance.InitDataBucket(mapper, config);
    }

    public void ExampleStartGame()
    {
        DataBucketController.Instance.HandleGameEvent(DataBucketEngineEvents.StageLoadStart);
        DataBucketController.Instance.LogLevelStart();
    }


    public void ExampleEndGame()
    {
        var isWin = true;
        if (!isWin)
        {
            DataBucketController.Instance.HandleGameEvent(DataBucketEngineEvents.LoseLevel);
        }

        DataBucketController.Instance.LogLevelEnd();
    }
    

    public void ApplicationPause(bool PauseStatus)
    {
        DataBucketController.Instance.HandleFlowUserExitGame(PauseStatus);
    }

    public void ExampleLogResourceEarnHomeWeapon()
    {
        var id = "";
        var type = "";
        var quantity = 1;

        DataBucketController.Instance.LogResourcesEarn(id, type, quantity, ResourceLogParams.EarnHomeWeapon());
    }
}
