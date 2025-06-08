using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public EnemyStats enemyStats;

    public TMP_Text playerHPText;  
    public TMP_Text enemyHPText;   
    public TMP_Text logText;       
    public Button attackButton;
    public Button defendButton;
    public Button runButton;


    private bool playerIsDefending = false;
    private bool playerTurn = true;
    private bool battleEnded = false;
    public SpriteRenderer playerSprite;
    public SpriteRenderer enemySprite;

    void Start()
    {
        UpdateUI();
        logText.text = $"A {enemyStats.enemyName} appeared!";
    }

    public void OnAttack()
    {
        if (!playerTurn || battleEnded || enemyStats.currentHealth <= 0) return;

        int damage = Mathf.Max(playerStats.data.attack - enemyStats.defense, 1);
        enemyStats.TakeDamage(damage);
        StartCoroutine(FlashRed(enemySprite));


        logText.text = $"You hit {enemyStats.enemyName} for {damage} damage!";
        UpdateUI();

        playerTurn = false;
        Invoke("EnemyTurn", 1.5f);
    }

    public void OnDefend()
    {
        if (!playerTurn || battleEnded) return;

        playerIsDefending = true;
        logText.text = "You defend for the next attack!";
        playerTurn = false;

        Invoke("EnemyTurn", 1.5f);
    }
    public void OnRun()
    {
        if (!playerTurn || battleEnded) return;

        bool escaped = Random.value < 0.5f; // 50% chance to run
        if (escaped)
        {
            logText.text = "You escaped successfully!";
            battleEnded = true;
            attackButton.interactable = false;

            if (defendButton != null) defendButton.interactable = false;
            if (runButton != null) runButton.interactable = false;

            Invoke("ReturnToPreviousScene", 1.5f);
        }
        else
        {
            logText.text = "You tried to run but failed!";
            playerTurn = false;
            Invoke("EnemyTurn", 1.5f);
        }
    }

    void EnemyTurn()
    {
        if (battleEnded || enemyStats.currentHealth <= 0) return;

        // Base damage
        int baseDamage = Mathf.Max(enemyStats.attack - playerStats.data.defense, 1);

        // Randomly decide strong or normal attack
        bool useStrongAttack = Random.value < 0.3f; // 30% chance
        int damage = useStrongAttack ? Mathf.FloorToInt(baseDamage * 1.5f) : baseDamage;

        // Check if player is defending
        if (playerIsDefending)
        {
            damage = Mathf.FloorToInt(damage * 0.5f);
            playerIsDefending = false;
        }

        playerStats.TakeDamage(damage);
        StartCoroutine(FlashRed(playerSprite));

        string action = useStrongAttack ? "uses a strong attack" : "attacks";
        logText.text = $"{enemyStats.enemyName} {action} for {damage} damage!";

        UpdateUI();
        playerTurn = true;
        CheckEndBattle();
    }



    void UpdateUI()
    {
        playerHPText.text = $"Player HP: {playerStats.data.currentHealth}/{playerStats.data.maxHealth}";
        enemyHPText.text = $"{enemyStats.enemyName} HP: {enemyStats.currentHealth}/{enemyStats.maxHealth}";
    }

    void CheckEndBattle()
    {
        if (playerStats.data.currentHealth <= 0)
        {
            logText.text = "You were defeated...";
            EndBattle();
        }
        else if (enemyStats.currentHealth <= 0)
        {
            logText.text = $"You defeated {enemyStats.enemyName}!";
            EndBattle();
        }
    }
    IEnumerator FlashRed(SpriteRenderer sprite)
    {
        Color originalColor = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
    }

    void EndBattle()
    {
        battleEnded = true;
        attackButton.interactable = false;
    }
    public void ReturnToPreviousScene()
    {
        if (!string.IsNullOrEmpty(BattleMemory.previousSceneName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(BattleMemory.previousSceneName);
        }
        else
        {
            Debug.LogWarning("BattleMemory.previousSceneName is empty!");
        }
    }

}
