using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public InventoryData inventoryData;
    public StoryModeData csSoryModeData;
    public PlayerData playerData;
}

[Serializable]
public class PlayerData
{
    public string userId;
    public int level;
    public string name;
}

[Serializable]
public class StoryModeData
{
    public int currentMap;
    public int currentMapSelected;
    public int difficultySelected;
    public int listMapStoryMapDetail => 5;
}

[Serializable]
public class InventoryData
{
    public ShipData gun;
    public ShipData droneLeft;
    public ShipData droneRight;
    
    public ShipData GetCurrentShipData()
    {
        return new ShipData()
        {
            id = "No.1",
            level = 1,
            grade = 1,
        };
    }
}

[Serializable]
public class ShipData
{
    public string id;
    public int level;
    public int grade;
}
