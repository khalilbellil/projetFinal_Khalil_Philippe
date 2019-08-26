using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : MonoBehaviour
{
    public string pnjName;
    public Quest myQuest;
    public bool dialogueIsOpen;
    public bool questAccepted;
    public LayerMask talkableLayer;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.OpenClosePressKeyUI(); //Activate interaction UI

    }
    private void OnTriggerStay(Collider other)
    {
        //if interaction pressed -> activate dialogue ui with the pnj quest -> And Desactivate interaction UI
        if (InputManager.Instance.inputPressed.interactPressed)
        {
            if (!questAccepted)
            {
                UIManager.Instance.CreateDialogue(pnjName, myQuest.description);
            }
            UIManager.Instance.OpenCloseDialogue(this);
            UIManager.Instance.OpenClosePressKeyUI();
        }

        if (!questAccepted && dialogueIsOpen)
        {
            QuestManager.Instance.AcceptQuest(this, InputManager.Instance.inputPressed.leftMouseButtonPressed);
            QuestManager.Instance.DeclineQuest(this, InputManager.Instance.inputPressed.rightMouseButtonPressed);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.uiLinks.pressKeyUI.SetActive(false); //Desactivate interaction UI
        UIManager.Instance.uiLinks.dialogueUI.SetActive(false); //Desactivate dialogue UI
        dialogueIsOpen = false;
    }

}
