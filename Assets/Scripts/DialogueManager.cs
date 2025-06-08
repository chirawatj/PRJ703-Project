using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public Image portraitImage;

    private NPC currentNPC;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCurrentNPC(NPC npc)
    {
        currentNPC = npc;
        StartDialogue(npc);
    }

    private void StartDialogue(NPC npc)
    {
        dialoguePanel.SetActive(true);
        nameText.text = npc.dialogueData.npcName;
        portraitImage.sprite = npc.dialogueData.npcPortrait;

        StopAllCoroutines();
        StartCoroutine(TypeLine(npc));
    }

    private IEnumerator TypeLine(NPC npc)
    {
        string line = npc.dialogueData.dialogueLines[0];
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(npc.dialogueData.typingSpeed);
        }
    }

    public void EndCurrentDialogue()
    {
        if (currentNPC != null)
        {
            currentNPC.EndDialogue();
            currentNPC = null;
        }

        dialoguePanel.SetActive(false); 
        dialogueText.text = "";
    }

}
