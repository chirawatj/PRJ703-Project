using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterTabUI : MonoBehaviour
{
    public TextMeshProUGUI NameValue;
    public TextMeshProUGUI LevelValue;
    public TextMeshProUGUI HealthValue;
    public TextMeshProUGUI AttackValue;
    public TextMeshProUGUI defenseValue;

    private PlayerStats playerStats;

    void OnEnable()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        UpdateUI();
    }

    void UpdateUI()
    {
        NameValue.text = playerStats.data.playerName;
        LevelValue.text = playerStats.data.level.ToString();
        HealthValue.text = $"{playerStats.data.currentHealth} / {playerStats.data.maxHealth}";
        AttackValue.text = playerStats.data.attack.ToString();
        defenseValue.text = playerStats.data.defense.ToString();
    }
}

