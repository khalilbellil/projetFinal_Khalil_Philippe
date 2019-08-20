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
        //mainEntry = GameObject.FindObjectOfType<MainEntry>();
        uiLinks = GameObject.FindObjectOfType<UILinks>();
    }

    public void UpdateManager()
    {
        TestEnnemiHpBar();
        OpenCloseQuests();
    }

    public void FixedUpdateManager()
    {

    }

    public void StopManager()
    {//Reset everything
        instance = null;
    }

    //QUESTS FUNCTIONS:

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

    public void OpenCloseQuests()
    {
        if (InputManager.Instance.inputPressed.questsPressed)
        {
            if (!uiLinks.QuestsUI.activeSelf)
            {
                uiLinks.QuestsUI.SetActive(true);
            }
            else
            {
                uiLinks.QuestsUI.SetActive(false);
            }
            
        }
    }

    //DIALOGUE FUNCTIONS:

    public void CreateDialogue(string pnjName, string dialogueText)
    {
        uiLinks.PnjNameText.text = pnjName;
        uiLinks.dialogueText.text = dialogueText;
    }

    public void OpenCloseDialogue()
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


    //DEBUG FUNCTIONS:

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
