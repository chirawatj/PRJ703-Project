using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public string mapBoundary;
    public string sceneName;
    public List<InventorySaveData> inventorySaveData;
    public List<ChestSaveData> chestSaveData;
}

[System.Serializable]
public class ChestSaveData
{
    public string chestID;
    public bool isOpened;
}

