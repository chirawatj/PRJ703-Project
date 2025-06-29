using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string sceneToLoad;       
    public string entryPointName;    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneTransition.lastExitPoint = entryPointName;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
