using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dialogue : MonoBehaviour
{
    PNJ attachedPnj;

    public List<string> dialogues;
    int actualDialogueIndex;
    bool dialogueIsOpen;

    private void Start()
    {
        attachedPnj = GetComponent<PNJ>();
        actualDialogueIndex = 0;
        dialogueIsOpen = false;
    }

    private void Update()
    {
        if (dialogueIsOpen)
        {
            UIManager.Instance.OpenDialogueUI();
            if (InputManager.Instance.inputPressed.leftMouseButtonPressed && actualDialogueIndex == 1)
            {
                LaunchNextDialogue();
            }
        }
        else
        {
            UIManager.Instance.CloseDialogueUI();
        }
    }

    public void LaunchNextDialogue()
    {
        if (actualDialogueIndex < dialogues.Count)
        {
            UIManager.Instance.SetDialogueUI(attachedPnj.pnjName, dialogues[actualDialogueIndex]);
            actualDialogueIndex++;

            if (!dialogueIsOpen)
            {
                dialogueIsOpen = true;
            }
        }
    }

}
