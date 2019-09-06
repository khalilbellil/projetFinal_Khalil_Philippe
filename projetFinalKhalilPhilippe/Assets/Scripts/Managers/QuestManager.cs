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

    public void AcceptQuest(PNJ pnj)
    {
        activeQuest = pnj.myQuest;
        pnj.questAccepted = true;
        PlayerManager.Instance.player.NotifyPlayer(pnj.myQuest.questName + " Quest ACCEPTED", 4); //Notify the player with UI

        //if (!myActiveQuests.ContainsKey(0))
        //{
        //    myActiveQuests[0] = pnj.myQuest;
        //    pnj.questAccepted = true;
        //    PlayerManager.Instance.player.NotifyPlayer("Quest ACCEPTED", 4); //Notify the player with UI
        //}
        //else if (!myActiveQuests.ContainsKey(1))
        //{
        //    myActiveQuests[1] = pnj.myQuest;
        //    pnj.questAccepted = true;
        //    PlayerManager.Instance.player.NotifyPlayer("Quest ACCEPTED", 4); //Notify the player with UI
        //}
        //else if (!myActiveQuests.ContainsKey(2))
        //{
        //    myActiveQuests[2] = pnj.myQuest;
        //    pnj.questAccepted = true;
        //    PlayerManager.Instance.player.NotifyPlayer("Quest ACCEPTED", 4); //Notify the player with UI
        //}
    }

    public void DeclineQuest(Quest questToDecline)
    {
        PlayerManager.Instance.player.NotifyPlayer(questToDecline.questName + " Quest DECLINED", 4); //Notify the player with UI
    }

    public void CompleteQuest()
    {
        activeQuest.isAchieved = true;
        myAchivedQuests.Add(myAchivedQuests.Count, activeQuest);
        PlayerManager.Instance.player.NotifyPlayer(activeQuest.questName + " Quest COMPLETED !", 4); //Notify the player with UI
        activeQuest = null;

        //myActiveQuests[questKey].isAchieved = true;

        ////Transfering the quest from myActiveQuest to MyAchivedQuests:
        //myAchivedQuests.Add(myAchivedQuests.Count, myActiveQuests[questKey]);
        //myActiveQuests.Remove(questKey);

        //PlayerManager.Instance.player.NotifyPlayer("Quest COMPLETED !", 4); //Notify the player with UI
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

    public void NotifyQuestThatPnjWasTalkedTo()
    {

    }
}
