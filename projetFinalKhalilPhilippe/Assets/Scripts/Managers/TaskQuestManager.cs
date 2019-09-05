using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TaskEventHandler();
public class TaskQuestManager
{

    #region Singleton Pattern
    private static TaskQuestManager instance = null;
    private TaskQuestManager() { }
    public static TaskQuestManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TaskQuestManager();
            }
            return instance;
        }
    }
    #endregion

    public Quest trackedQuest;

    //public Dictionary<string, Stack<QuestTask>> currentTasks;


    public void Initialize()
    {
        trackedQuest = null;
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

    
}
