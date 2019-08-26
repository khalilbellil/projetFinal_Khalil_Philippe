using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    Primary, Secondary
}

public class QuestManager
{
    public Dictionary<int, Quest> myActiveQuests;
    public Dictionary<int, Quest> myAchivedQuests;

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
        myActiveQuests = new Dictionary<int, Quest>();
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

    //public void AcceptQuest(PNJ pnjWhoGiveQuest, bool inputPressedToAccept)
    //{
    //    if (inputPressedToAccept && !pnjWhoGiveQuest.questAccepted)
    //    {
    //        myActiveQuests.Add(myActiveQuests.Count, pnjWhoGiveQuest.myQuest);
    //        pnjWhoGiveQuest.questAccepted = true;
    //        UIManager.Instance.CreateDialogue(pnjWhoGiveQuest.pnjName, "QUEST ACCEPTED, good luck !");
    //    }
    //}

    //public void DeclineQuest(PNJ pnjWhoGiveQuest, bool inputPressedToDecline)
    //{
    //    if (inputPressedToDecline && !pnjWhoGiveQuest.questAccepted)
    //    {
    //        pnjWhoGiveQuest.questAccepted = false;
    //        UIManager.Instance.CreateDialogue(pnjWhoGiveQuest.pnjName, "QUEST DECLINED, come back when you are ready...");
    //    }
    //}

    public void CompleteQuest(int questKey)
    {
        myActiveQuests[questKey].isAchieved = true; //Update the scriptable object

        //Transfering the quest from myActiveQuest to MyAchivedQuests:
        myAchivedQuests.Add(myAchivedQuests.Count, myActiveQuests[questKey]);
        myActiveQuests.Remove(questKey);
    }

}
