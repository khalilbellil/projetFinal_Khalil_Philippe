using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyQuest : Quest
{
    public int nbOfEnnemies = 1;

    protected override void SetupTasks()
    {
        QuestTask q1 = new QuestTask();
        q1.triggerExpected = nbOfEnnemies;
        q1.description = "Kill " + nbOfEnnemies + " enemies";
        q1.onTaskStart = () => { EnnemyManager.Instance.AddQuestEnemies(nbOfEnnemies, q1.SubTaskCompleted); };
        q1.cleanAll = () => { EnnemyManager.Instance.CleanUpQuestEnemies(); };

        tasks = new Queue<QuestTask>();
        tasks.Enqueue(q1);
        base.SetupTasks();

        Debug.Log(questName + " was setup.");
    }
}
