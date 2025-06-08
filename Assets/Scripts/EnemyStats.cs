using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string enemyName = "Slime";
    public int maxHealth = 30;
    public int currentHealth;
    public int attack = 3;
    public int defense = 1;

    void Awake()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
    }
}
