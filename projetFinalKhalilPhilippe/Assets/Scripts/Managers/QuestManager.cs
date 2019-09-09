using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    Primary, Secondary
}

public class QuestManager
{
    #region Singleton Pattern
    private static QuestManager instance = null;
    private QuestManager() { }
    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new QuestManager();
            }
            return instance;
        }
    }
    #endregion

    //public Dictionary<int, Quest> myActiveQuests;
    public Quest activeQuest;
    public Dictionary<int, Quest> myAchivedQuests;

    public void Initialize()
    {
        //myActiveQuests = new Dictionary<int, Quest>();
        activeQuest = null;
        myAchivedQuests = new Dictionary<int, Quest>();
        
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

    // FUNCTIONS //

    public void AcceptQuest()
    {
        activeQuest = PlayerManager.Instance.player.target.GetComponent<PNJ>().myQuest;
        PlayerManager.Instance.player.target.GetComponent<PNJ>().questAccepted = true;
        PlayerManager.Instance.player.NotifyPlayer(PlayerManager.Instance.player.target.GetComponent<PNJ>().myQuest.questName + " Quest ACCEPTED", 4); //Notify the player with UI
        UIManager.Instance.CloseYesOrNoUI();
        DialogueManager.Instance.FinishDialogue();
    }

    public void DeclineQuest()
    {
        PlayerManager.Instance.player.NotifyPlayer(PlayerManager.Instance.player.target.GetComponent<PNJ>().myQuest.questName + " Quest DECLINED", 4); //Notify the player with UI
        UIManager.Instance.CloseYesOrNoUI();
        DialogueManager.Instance.FinishDialogue();
    }

    public void CompleteQuest()
    {
        activeQuest.isAchieved = true;
        myAchivedQuests.Add(myAchivedQuests.Count, activeQuest);
        PlayerManager.Instance.player.NotifyPlayer(activeQuest.questName + " Quest COMPLETED !", 4); //Notify the player with UI
        activeQuest = null;
    }

    public void NotifyQuestThatEnnemyWasKilled()
    {
        if (activeQuest != null)
        {
            if (activeQuest.killEnnemyTaskActive)
            {
                activeQuest.nbEnnemiesToKill--;
            }
        }
    }
}
