using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : MonoBehaviour
{
    public string pnjName;
    public Quest attachedQuest;
    Dialogue pnjDialogue;

    private void Start()
    {
        pnjDialogue = GetComponent<Dialogue>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.OpenClosePressKeyUI();
        }
        

    }
    private void OnTriggerStay(Collider other)
    {
        if (InputManager.Instance.inputPressed.interactPressed && other.CompareTag("Player"))
        {
            UIManager.Instance.uiLinks.dialogueUI.SetActive(false);
            UIManager.Instance.uiLinks.dialogueUI.SetActive(true);
            pnjDialogue.LaunchDialogue(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.uiLinks.pressKeyUI.SetActive(false);
            UIManager.Instance.uiLinks.dialogueUI.SetActive(false);
        }
    }
}

