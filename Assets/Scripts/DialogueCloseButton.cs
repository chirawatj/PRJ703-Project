using UnityEngine;
using UnityEngine.UI;

public class DialogueCloseButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            DialogueManager.Instance.EndCurrentDialogue();
        });
    }
}
