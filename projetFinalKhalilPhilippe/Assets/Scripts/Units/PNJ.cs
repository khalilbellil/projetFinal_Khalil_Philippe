using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : BaseUnit
{
    public bool pnjToTalk;
    public string pnjName;
    public List<string> dialogue;
    [HideInInspector]
    public Quest myQuest;
    [HideInInspector]
    public bool thereIsQuestToPropose;
    [HideInInspector]
    public bool dialogueIsOpen;
    [HideInInspector]
    public bool questAccepted;
    [HideInInspector]
    public QuestTracker questTracker;
    

    private void Start()
    {
        base.Init();
        unitName = pnjName;

        InitQuest();
        InitQuestTracker();
        
    }

    private void Update()
    {
        
    }

    void InitQuest()
    {
        myQuest = GetComponent<Quest>();
        if (myQuest != null)
        {
            thereIsQuestToPropose = true;
        }
        else
        {
            thereIsQuestToPropose = false;
        }
    }

    void InitQuestTracker()
    {
        if (pnjToTalk)
        {
            questTracker = GetComponent<QuestTracker>();
            if (questTracker)
            {
                pnjToTalk = true;
            }
            else
            {
                pnjToTalk = false;
            }
        }

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
            other.gameObject.GetComponent<Player>().target = null;
            other.gameObject.GetComponent<Player>().pressKeyAvailable = false;
        }
    }

}
