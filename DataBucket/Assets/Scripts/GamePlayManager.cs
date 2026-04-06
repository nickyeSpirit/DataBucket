using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;

    public CollectionInfo collectionInfo;
    public WaveManager waveManager;
    public GamePlayInfo currentGamePlayInfo;
    public PlayerManager playerManager;
    public bool didQuitByUser;
}

[Serializable]
public class PlayerManager
{
    public MountController mountController;
}

[Serializable]
public class MountController
{
    public int hp;
}

[Serializable]
public class CollectionInfo
{
    public int score;
    public bool isWin;
}

[Serializable]
public class WaveManager
{
    public int indexWave;
}

[Serializable]
public class GamePlayInfo
{
    public int GetTotalWaveThisStage() => 1;
}