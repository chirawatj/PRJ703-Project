using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    void Awake()
    {
        // Auto-find UI elements 
        if (dialoguePanel == null)
            dialoguePanel = GameObject.Find("DialoguePanel");

        if (dialogueText == null)
            dialogueText = GameObject.Find("DialogueText")?.GetComponent<TMP_Text>();

        if (nameText == null)
            nameText = GameObject.Find("NPCNameText")?.GetComponent<TMP_Text>();

        if (portraitImage == null)
            portraitImage = GameObject.Find("DialoguePortrait")?.GetComponent<Image>();
    }

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
            return;

        if (isDialogueActive)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
                isTyping = false;
            }
            else if (dialogueIndex + 1 < dialogueData.dialogueLines.Length)
            {
                dialogueIndex++;
                StartCoroutine(TypeLine());
            }
            else
            {
                DialogueManager.Instance.EndCurrentDialogue();
            }
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        DialogueManager.Instance.SetCurrentNPC(this);
        isDialogueActive = true;
        dialogueIndex = 0;
        PauseController.SetPause(true);

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;
        dialoguePanel.SetActive(true);

        StartCoroutine(TypeLine());
    }



    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (dialogueIndex + 1 < dialogueData.dialogueLines.Length)
        {
            dialogueIndex++;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;

        PauseController.SetPause(false);
    }
}
