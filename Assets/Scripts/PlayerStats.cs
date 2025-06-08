using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerData data = new PlayerData();

    public void TakeDamage(int amount)
    {
        data.currentHealth = Mathf.Max(data.currentHealth - amount, 0);
    }

    public void Heal(int amount)
    {
        data.currentHealth = Mathf.Min(data.currentHealth + amount, data.maxHealth);
    }
}
