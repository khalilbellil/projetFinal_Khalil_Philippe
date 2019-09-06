using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Quest : MonoBehaviour
{
    public Queue<QuestTask> tasks;

    public bool isAchieved;
    public bool questStarted;

    public string questName;
    public string description;
    public int requiredLvl;

    [Header("Kill Ennemy Task:")]
    public bool killEnnemyTaskActive;
    public int nbEnnemiesToKill;

    [Header("Talk To Task:")]
    public bool talkToTaskActive;
    public List<string> pnjNamesToTalk;
    public bool talkToDone;


    private void Start()
    {
        //Init:
        isAchieved = false;
        questStarted = false;
    }

    private void Update()
    {
        UpdateTasks();
    }

    public void UpdateTasks()
    {
        if (!isAchieved &&killEnnemyTaskActive)
        {
            if (nbEnnemiesToKill == 0)
            {
                if (!talkToTaskActive)
                {
                    QuestManager.Instance.CompleteQuest();
                }
                else
                {
                    if (pnjNamesToTalk.Count == 0)
                    {
                        QuestManager.Instance.CompleteQuest();
                    }
                }
            }
        }
    }

    protected virtual void SetupTasks()
    {
        QuestTask q1 = new QuestTask();
        q1.triggerExpected = 1;
        q1.description = "Return to the PNJ who gave you the quest";
        q1.onTaskStart = () => { GetComponent<PNJ>().talkTo += q1.SubTaskCompleted; };
        q1.cleanAll = () => { GetComponent<PNJ>().talkTo -= q1.SubTaskCompleted; };
        tasks.Enqueue(q1);
        Debug.Log("Return to PNJ task was setup.");
    }

}
