using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Quest : MonoBehaviour
{
    [HideInInspector]
    public bool isAchieved;
    [HideInInspector]
    public bool questStarted;

    public string questName;
    public string description;
    public int requiredLvl;

    public Item itemToGive;

    [Header("Kill Ennemy Task:")]
    public bool killEnnemyTaskActive;
    public int nbEnnemiesToKill;

    [Header("Talk To Task:")]
    public bool talkToTaskActive;
    public List<string> pnjNamesToTalk;
    [HideInInspector]
    public bool talkToDone;

    [Header("PickUp Item Task:")]
    public bool pickUpItemTaskActive;
    public Item itemToPickUp;
    [HideInInspector]
    public bool itemPickedUpDone;


    private void Start()
    {
        //Init:
        isAchieved = false;
        questStarted = false;
        InitTasks();
    }

    private void Update()
    {
        UpdateTasksStatus();
    }

    public void InitTasks()
    {
        if (!killEnnemyTaskActive)
        {
            nbEnnemiesToKill = 0;
        }
        if (!talkToTaskActive)
        {
            talkToDone = true;

        }
        if (!pickUpItemTaskActive)
        {
            itemPickedUpDone = true;
        }
    }

    public void UpdateTasksStatus()
    {
        if (!isAchieved)
        {
            if (nbEnnemiesToKill == 0 && talkToDone && itemPickedUpDone)
            {
                QuestManager.Instance.CompleteQuest(itemToGive);
            }
        }
    }

    public bool IsOtherTasksDone()
    {
        if (nbEnnemiesToKill == 0 && itemPickedUpDone)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
