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
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerManager.Instance.player.TakeDamage(10);
            Debug.Log("new hp amount: " + PlayerManager.Instance.player.health);
        }
        float a = (float)PlayerManager.Instance.player.health / PlayerManager.Instance.player.maxHealth;
        
        uiLinks.healthBar.fillAmount = a;

        OpenQuests();
        TestDialogue();
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

        }
        if (uiLinks.quest2Text.text != "")
        {

        }
        if (uiLinks.quest3Text.text != "")
        {

        }
    }

    void OpenQuests()
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

    void TestDialogue()
    {
        if (InputManager.Instance.inputPressed.openDialogueTempPressed)
        {
            if (!uiLinks.dialogueUI.activeSelf)
            {
                uiLinks.dialogueUI.SetActive(true);
            }
            else
            {
                uiLinks.dialogueUI.SetActive(false);
            }
        }
    }

}
