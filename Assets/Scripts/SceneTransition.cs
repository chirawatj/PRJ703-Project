using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public static string lastExitPoint = "";

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
