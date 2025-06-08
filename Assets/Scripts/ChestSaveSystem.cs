using System.Collections.Generic;

public static class ChestSaveSystem
{
    private static Dictionary<string, bool> chestStates = new Dictionary<string, bool>();

    public static void Load(List<ChestSaveData> savedData)
    {
        chestStates.Clear();
        foreach (var data in savedData)
        {
            chestStates[data.chestID] = data.isOpened;
        }
    }

    public static List<ChestSaveData> GetAllChestSaveData()
    {
        List<ChestSaveData> data = new List<ChestSaveData>();
        foreach (var entry in chestStates)
        {
            data.Add(new ChestSaveData
            {
                chestID = entry.Key,
                isOpened = entry.Value
            });
        }
        return data;
    }

    public static bool IsChestOpened(string chestID)
    {
        return chestStates.TryGetValue(chestID, out bool opened) && opened;
    }

    public static void MarkChestAsOpened(string chestID)
    {
        chestStates[chestID] = true;
    }
}
