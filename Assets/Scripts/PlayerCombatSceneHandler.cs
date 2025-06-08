using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombatSceneHandler : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "CombatScene")
        {
            gameObject.SetActive(false);
        }
    }
}
