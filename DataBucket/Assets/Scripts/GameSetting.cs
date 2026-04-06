using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public static GamePlayMode gamePlayMode;
    
    public enum GamePlayMode
    {
        Endless = 0, Tutorial = 1, StoryMode = 5, BossRaid = 8
    }
}


