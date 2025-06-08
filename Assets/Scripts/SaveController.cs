using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private static SaveController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        LoadGame(); // Load game automatically on start
    }

    public void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CinemachineConfiner confiner = FindObjectOfType<CinemachineConfiner>();
        InventoryController inventory = FindObjectOfType<InventoryController>();

        if (player != null && confiner != null && confiner.m_BoundingShape2D != null)
        {
            SaveData saveData = new SaveData
            {
                playerPosition = player.transform.position,
                mapBoundary = confiner.m_BoundingShape2D.gameObject.name,
                sceneName = SceneManager.GetActiveScene().name,
                inventorySaveData = inventory != null ? inventory.GetInventoryItems() : new List<InventorySaveData>(),
                chestSaveData = ChestSaveSystem.GetAllChestSaveData()
            };

            File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
            Debug.Log("Game saved to: " + saveLocation);
        }
        else
        {
            Debug.LogWarning("Cannot save: player or confiner not found.");
        }
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            // Load global systems before loading scene
            ChestSaveSystem.Load(saveData.chestSaveData);

            StartCoroutine(LoadSceneAndRestore(saveData));
        }
        else
        {
            Debug.Log("No save file found. Saving new game state.");
            SaveGame(); // Creates a save file if none exists
        }
    }

    private IEnumerator LoadSceneAndRestore(SaveData saveData)
    {
        if (string.IsNullOrEmpty(saveData.sceneName))
        {
            Debug.LogError("Scene name is missing or empty in save file!");
            yield break;
        }

        // Load the saved scene
        SceneManager.LoadScene(saveData.sceneName);
        yield return new WaitForSeconds(0.1f); 

        // Restore player position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = saveData.playerPosition;
        }
        else
        {
            Debug.LogWarning("Player not found in scene: " + saveData.sceneName);
        }

        // Restore camera confiner
        CinemachineConfiner confiner = FindObjectOfType<CinemachineConfiner>();
        if (confiner != null)
        {
            GameObject bound = GameObject.Find(saveData.mapBoundary);
            if (bound != null)
            {
                confiner.m_BoundingShape2D = bound.GetComponent<PolygonCollider2D>();
                confiner.InvalidatePathCache();
            }
            else
            {
                Debug.LogWarning("Boundary not found: " + saveData.mapBoundary);
            }
        }
        else
        {
            Debug.LogWarning("CinemachineConfiner not found.");
        }

        // Restore inventory after scene has loaded
        InventoryController inventory = FindObjectOfType<InventoryController>();
        if (inventory != null)
        {
            inventory.SetInventoryItems(saveData.inventorySaveData);
        }
        else
        {
            Debug.LogWarning("InventoryController not found in loaded scene.");
        }
    }
}
