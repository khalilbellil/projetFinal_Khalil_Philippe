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

    public int nbOfQuestsToWin;

    //public Dictionary<int, Quest> myActiveQuests;
    public Quest activeQuest;
    public List<Quest> myAchivedQuests;

    public void Initialize()
    {
        //myActiveQuests = new Dictionary<int, Quest>();
        activeQuest = null;
        myAchivedQuests = new List<Quest>();
        nbOfQuestsToWin = GameObject.FindObjectsOfType<Quest>().Length;
        
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
        if (activeQuest == null)
        {
            activeQuest = PlayerManager.Instance.player.target.GetComponent<PNJ>().myQuest;
            PlayerManager.Instance.player.target.GetComponent<PNJ>().questAccepted = true;
            PlayerManager.Instance.player.NotifyPlayer(PlayerManager.Instance.player.target.GetComponent<PNJ>().myQuest.questName + " Quest ACCEPTED", 4); //Notify the player with UI
            UIManager.Instance.CloseYesOrNoUI();
            DialogueManager.Instance.FinishDialogue();
            PlayerManager.Instance.player.target.GetComponent<PNJ>().thereIsQuestToPropose = false;
        }
        else
        {
            PlayerManager.Instance.player.NotifyPlayer("You already have an active quest !", 4);
            PlayerManager.Instance.player.target.GetComponent<PNJ>().thereIsQuestToPropose = true;
        }

    }

    public void DeclineQuest()
    {
        PlayerManager.Instance.player.NotifyPlayer(PlayerManager.Instance.player.target.GetComponent<PNJ>().myQuest.questName + " Quest DECLINED", 4); //Notify the player with UI
        UIManager.Instance.CloseYesOrNoUI();
        DialogueManager.Instance.FinishDialogue();
    }

    public void CompleteQuest(Item itemToAdd)
    {
        activeQuest.isAchieved = true;
        myAchivedQuests.Add(activeQuest);
        PlayerManager.Instance.player.NotifyPlayer(activeQuest.questName + " Quest COMPLETED !", 4); //Notify the player with UI
        UIManager.Instance.uiLinks.questText.text = "";
        activeQuest = null;

        if (itemToAdd != null)
        {
            PlayerManager.Instance.player.inventory.AddItem(itemToAdd);
        }
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
