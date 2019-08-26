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

    public UILinks uiLinks;

    public void Initialize()
    {
        uiLinks = GameObject.FindObjectOfType<UILinks>();
    }

    public void UpdateManager()
    {
        TestEnnemiHpBar();
        OpenCloseQuestsUI();
    }

    public void FixedUpdateManager()
    {

    }

    public void StopManager()
    {//Reset everything
        instance = null;
    }

    // QUESTS FUNCTIONS //

    public void AddQuestToUI(Quest _quest)
    {
        if (uiLinks.quest1Text.text != "")
        {
            uiLinks.quest1Text.text = _quest.description;
        }
        else if (uiLinks.quest2Text.text != "")
        {
            uiLinks.quest2Text.text = _quest.description;
        }
        else if (uiLinks.quest3Text.text != "")
        {
            uiLinks.quest3Text.text = _quest.description;
        }
    }

    public void OpenCloseQuestsUI()
    {
        if (InputManager.Instance.inputPressed.questsPressed)
        {
            if (!uiLinks.QuestsUI.activeSelf)
            {
                LoadMyQuestInUI();

                uiLinks.QuestsUI.SetActive(true);
            }
            else
            {
                uiLinks.QuestsUI.SetActive(false);
            }
            
        }
    }
    
    public void LoadMyQuestInUI()
    {
        if (QuestManager.Instance.myActiveQuests.ContainsKey(0))
        {
            UIManager.instance.uiLinks.quest1Title.text = QuestManager.Instance.myActiveQuests[0].questName;
            UIManager.instance.uiLinks.quest1Text.text = QuestManager.Instance.myActiveQuests[0].description;
        }

        if (QuestManager.Instance.myActiveQuests.ContainsKey(1))
        {
            UIManager.instance.uiLinks.quest2Title.text = QuestManager.Instance.myActiveQuests[1].questName;
            UIManager.instance.uiLinks.quest2Text.text = QuestManager.Instance.myActiveQuests[1].description;
        }

        if (QuestManager.Instance.myActiveQuests.ContainsKey(2))
        {
            UIManager.instance.uiLinks.quest3Title.text = QuestManager.Instance.myActiveQuests[2].questName;
            UIManager.instance.uiLinks.quest3Text.text = QuestManager.Instance.myActiveQuests[2].description;
        }
    }


    //DIALOGUE FUNCTIONS //

    public void SetDialogueUI(string title, string dialogueText)
    {
        uiLinks.PnjNameText.text = title;
        uiLinks.dialogueText.text = dialogueText;
    }

    public void OpenCloseDialogueUI()
    {
        if (uiLinks.dialogueUI.activeSelf)
        {
            uiLinks.dialogueUI.SetActive(false);
        }
        else
        {
            uiLinks.dialogueUI.SetActive(true);
        }
        
    }

    public void CloseDialogueUI()
    {
        uiLinks.dialogueUI.SetActive(false);
    }

    public void OpenDialogueUI()
    {
        uiLinks.dialogueUI.SetActive(true);
    }

    public void OpenClosePressKeyUI()
    {
        if (uiLinks.pressKeyUI.activeSelf)
        {
            uiLinks.pressKeyUI.SetActive(false);
        }
        else
        {
            uiLinks.pressKeyUI.SetActive(true);
        }
    }

    public void ClosePressKeyUI()
    {
        uiLinks.pressKeyUI.SetActive(false);
    }

    public void OpenPressKeyUI()
    {
        uiLinks.pressKeyUI.SetActive(true);
    }


    //DEBUG FUNCTIONS //

    //Temporary HealthBar for an Ennemi
    void TestEnnemiHpBar()
    {
        float hpLeft = EnnemyManager.Instance.ennemy.health / EnnemyManager.Instance.ennemy.maxHealth;
        EnnemyManager.Instance.ennemy.transform.GetChild(0).localScale = new Vector3(hpLeft, 1, 1);
    }

    void TestDamage()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerManager.Instance.player.TakeDamage(10);
            Debug.Log("new hp amount: " + PlayerManager.Instance.player.health);
        }
        float a = (float)PlayerManager.Instance.player.health / PlayerManager.Instance.player.maxHealth;

        uiLinks.healthBar.fillAmount = a;
    }

}
