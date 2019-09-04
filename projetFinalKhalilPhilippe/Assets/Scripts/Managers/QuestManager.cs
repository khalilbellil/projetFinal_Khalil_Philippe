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

    public Dictionary<int, Quest> myActiveQuests;
    public Dictionary<int, Quest> myAchivedQuests;

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

    public void AcceptQuest(Quest questToAccept)
    {
        if (myActiveQuests[0] == null)
        {
            myActiveQuests[0] = questToAccept;
        }
        else if (myActiveQuests[1] == null)
        {
            myActiveQuests[1] = questToAccept;
        }
        else if (myActiveQuests[2] == null)
        {
            myActiveQuests[2] = questToAccept;
        }
    }

    public void DeclineQuest(Quest questToDecline)
    {

    }

    public void CompleteQuest(int questKey)
    {
        myActiveQuests[questKey].isAchieved = true; //Update the scriptable object

        //Transfering the quest from myActiveQuest to MyAchivedQuests:
        myAchivedQuests.Add(myAchivedQuests.Count, myActiveQuests[questKey]);
        myActiveQuests.Remove(questKey);
    }

}
