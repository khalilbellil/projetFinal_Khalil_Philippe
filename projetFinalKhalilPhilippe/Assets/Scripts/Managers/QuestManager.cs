using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    Primary, Secondary
}

public class QuestManager
{
    public Dictionary<int, Quest> allQuests;

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
        LoadQuestsFromRsrc();
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

    void LoadQuestsFromRsrc()
    {
        List<Quest> quests = new List<Quest>(Resources.LoadAll<Quest>("Quests"));
        allQuests = new Dictionary<int, Quest>();
        for (int i = 0; i < quests.Count; i++)
        {
            allQuests.Add(i, quests[i]);
        }

        UIManager.Instance.uiLinks.quest1Text.text = allQuests[0].questName;

    }
    

}
