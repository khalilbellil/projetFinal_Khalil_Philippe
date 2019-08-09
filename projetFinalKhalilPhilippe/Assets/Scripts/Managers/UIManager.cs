using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{

    #region Singleton Pattern
    private static UIManager instance = null;
    private UIManager() { }
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }
    #endregion

    public UILinks uiLinks; //initialized by player (because can be instantiated after UIManager)

    public void Initialize()
    {

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

    void AddQuestToUI(Quest _quest)
    {
        if (uiLinks.quest1Text.text != "")
        {
            uiLinks.quest1Text.text = _quest.questName;
        }
        else if (uiLinks.quest2Text.text != "")
        {
            uiLinks.quest1Text.text = _quest.questName;
        }
        else if (uiLinks.quest3Text.text != "")
        {
            uiLinks.quest1Text.text = _quest.questName;
        }
    }

    void ToggleQuestUI()
    {

    }

    void ToggleDialogueUI(string _pnjName, string _description)
    {

    }

}
