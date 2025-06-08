using UnityEngine;

public class EventSystemController : MonoBehaviour
{
    private static EventSystemController instance;

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
        }
    }
}
