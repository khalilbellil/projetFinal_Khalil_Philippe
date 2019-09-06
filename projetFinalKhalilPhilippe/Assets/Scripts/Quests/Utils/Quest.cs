using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Quest : MonoBehaviour
{
    [HideInInspector]
    public bool isAchieved;
    public string questName;
    public string description;
    public int requiredLvl;

    public Queue<QuestTask> tasks;
    
    private void Start()
    {
        isAchieved = false;
        SetupTasks();

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
