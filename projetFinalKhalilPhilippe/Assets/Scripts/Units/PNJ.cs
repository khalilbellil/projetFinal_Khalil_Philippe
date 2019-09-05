using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : BaseUnit
{
    public string pnjName;
    public Quest myQuest;
    public bool thereIsQuestToPropose;
    public List<string> dialogue;
    public bool dialogueIsOpen;
    public bool questAccepted;
    public LayerMask talkableLayer;

    private void Start()
    {
        base.Init();
        unitName = pnjName;
        if (myQuest != null)
        {
            thereIsQuestToPropose = true;
        }
        else
        {
            thereIsQuestToPropose = false;
        }
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.uiLinks.pressKeyUI.SetActive(true);
            other.gameObject.GetComponent<Player>().target = this.gameObject;
            other.gameObject.GetComponent<Player>().pressKeyAvailable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.uiLinks.pressKeyUI.SetActive(false);
            other.gameObject.GetComponent<Player>().pressKeyAvailable = false;
        }
    }

}
