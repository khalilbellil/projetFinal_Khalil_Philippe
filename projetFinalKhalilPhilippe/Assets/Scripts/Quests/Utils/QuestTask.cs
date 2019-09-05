using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTask
{

    public int triggerExpected;
    public TaskEventHandler onTaskStart;
    public string description;
    public TaskEventHandler cleanAll;

    public void SubTaskCompleted()
    {
        //increase triggerExpected
        triggerExpected--;

        //if bypass triggerexpceted, tell quest?

    }

}
