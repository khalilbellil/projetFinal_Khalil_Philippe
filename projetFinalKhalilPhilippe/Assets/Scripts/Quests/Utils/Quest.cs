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
    
    private void OnEnable()
    {
        isAchieved = false;
        SetupTasks();

    }

    

    protected virtual void SetupTasks()
    {
        QuestTask q1 = new QuestTask();
        q1.triggerExpected = 3;
        q1.description = " ";
        q1.onTaskStart = () => { GetComponent<PNJ>().talkTo += q1.SubTaskCompleted; };
        q1.cleanAll = () => { /*remove listener*/ };
        tasks.Enqueue(q1);
    }

}
