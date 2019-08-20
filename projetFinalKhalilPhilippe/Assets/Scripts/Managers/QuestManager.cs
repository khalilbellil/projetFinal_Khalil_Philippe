using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    Primary, Secondary
}

public class QuestManager
{
    public Dictionary<int, Quest> playerActiveQuests;

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

    public void Initialize()
    {
        playerActiveQuests = new Dictionary<int, Quest>();
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

    public void AcceptQuest(PNJ pnjWhoGiveQuest, bool inputPressedToAccept)
    {
        if (inputPressedToAccept && !pnjWhoGiveQuest.questAccepted)
        {
            playerActiveQuests.Add(playerActiveQuests.Count, pnjWhoGiveQuest.myQuest);
            pnjWhoGiveQuest.questAccepted = true;
            Debug.Log("Quest accepted");
            UIManager.Instance.CreateDialogue(pnjWhoGiveQuest.pnjName, "QUEST ACCEPTED, good luck !");
        }
    }

    public void DeclineQuest(PNJ pnjWhoGiveQuest, bool inputPressedToDecline)
    {
        if (inputPressedToDecline && !pnjWhoGiveQuest.questAccepted)
        {
            pnjWhoGiveQuest.questAccepted = false;
            Debug.Log("Quest declined");
            UIManager.Instance.CreateDialogue(pnjWhoGiveQuest.pnjName, "QUEST DECLINED, come back when you are ready...");
        }
    }

    // DEBUG FUNCTIONS //



}
