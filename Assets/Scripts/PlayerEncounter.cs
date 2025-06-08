using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEncounter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Save player’s position before combat
            BattleMemory.playerReturnPosition = transform.position;
            BattleMemory.previousSceneName = SceneManager.GetActiveScene().name;
            // Load combat scene
            SceneManager.LoadScene("CombatScene");
        }
    }
}
