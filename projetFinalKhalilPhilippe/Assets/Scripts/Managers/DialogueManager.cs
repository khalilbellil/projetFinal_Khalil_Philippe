using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager
{

    #region Singleton Pattern
    private static DialogueManager instance = null;
    private DialogueManager() { }
    public static DialogueManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DialogueManager();
            }
            return instance;
        }
    }
    #endregion

    List<string> actualDialogue;
    int actualDialogueIndex;
    string actualDialogueTitle;
    bool thereIsTextToShow;

    public void Initialize()
    {
        
    }

    public void UpdateManager()
    {

    }

    public void FixedUpdateManager()
    {

    }

    public void StopManager()
    {//Reset everything
        instance = null;
    }

    public void SetNewDialogue(List<string> newDialogue, string dialogueTitle)
    {
        actualDialogue = newDialogue;
        actualDialogueIndex = 0;
        actualDialogueTitle = dialogueTitle;
        thereIsTextToShow = true;
    }

    public void LaunchDialogue()
    {
        UIManager.Instance.CloseDialogueUI();
        UIManager.Instance.SetDialogueUI(actualDialogueTitle, actualDialogue[actualDialogueIndex]);
        UIManager.Instance.OpenDialogueUI();
        actualDialogueIndex++;

        if (actualDialogueIndex >= actualDialogue.Count)
        {
            thereIsTextToShow = false;
        }
        else
        {
            thereIsTextToShow = true;
        }
    }
    
    public void LaunchQuestDialogue(string pnjName, string questText)
    {
        UIManager.Instance.CloseDialogueUI();
        UIManager.Instance.SetDialogueUI(pnjName, questText);
        UIManager.Instance.OpenDialogueUI();
    }

    public void FinishDialogue()
    {
        UIManager.Instance.CloseDialogueUI();
        actualDialogue = null;
        actualDialogueTitle = null;
        actualDialogueIndex = 0;
    }
    
}
