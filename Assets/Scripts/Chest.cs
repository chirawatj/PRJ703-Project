using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }
    public string ChestID { get; private set; }
    public GameObject itemPrefab;
    public Sprite openedSprite;

    void Start()
    {
        // Generate unique ID if not set
        ChestID ??= GlobalHelper.GenerateUniqueID(gameObject);

        // Check if this chest has already been opened in saved data
        if (ChestSaveSystem.IsChestOpened(ChestID))
        {
            SetOpened(true);
        }
    }

    public bool CanInteract()
    {
        return !IsOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        OpenChest();
    }

    private void OpenChest()
    {
        SetOpened(true);

        // Save opened state
        ChestSaveSystem.MarkChestAsOpened(ChestID);

        // Give item to inventory
        InventoryController inventory = FindObjectOfType<InventoryController>();
        if (inventory != null && itemPrefab != null)
        {
            inventory.AddItemToInventory(itemPrefab);
        }
    }

    public void SetOpened(bool opened)
    {
        IsOpened = opened;
        if (IsOpened)
        {
            GetComponent<SpriteRenderer>().sprite = openedSprite;
        }
    }
}
