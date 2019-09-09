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

    public bool dialogueUIActive;

    public void Initialize()
    {
        uiLinks = GameObject.FindObjectOfType<UILinks>();
    }

    public void UpdateManager()
    {
        TestDamage();
        OpenCloseQuestsUI();
        OpenCloseInventoryUI();
    }

    public void FixedUpdateManager()
    {

    }

    public void StopManager()
    {//Reset everything
        instance = null;
    }

    // INVENTORY FUNCTIONS //

    public void OpenCloseInventoryUI()
    {
        if (InputManager.Instance.inputPressed.inventoryPressed)
        {
            if (!uiLinks.inventoryUI.activeSelf)
            {
                UIManager.Instance.SetStatsValuesInventoryUI(PlayerManager.Instance.player.range, PlayerManager.Instance.player.dmg, PlayerManager.Instance.player.speed, PlayerManager.Instance.player.maxHealth);

                uiLinks.inventoryUI.SetActive(true);
            }
            else
            {
                uiLinks.inventoryUI.SetActive(false);
            }

        }

    }

    public void SetStatsValuesInventoryUI(float range, float dammage, float speed, float maxHealth)
    {
        uiLinks.rangeValue.text = range.ToString();
        uiLinks.dammageValue.text = dammage.ToString();
        uiLinks.speedValue.text = speed.ToString();
        uiLinks.maxHealthValue.text = maxHealth.ToString();
    }

    public void LoadInventoryItemsUI()
    {

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
        if (QuestManager.Instance.activeQuest != null)
        {
            //UIManager.instance.uiLinks.quest1Title.text = QuestManager.Instance.activeQuest.questName;
            UIManager.instance.uiLinks.quest1Text.text = QuestManager.Instance.activeQuest.description;
        }
    }


    //DIALOGUE FUNCTIONS //

    public void OpenCloseYesOrNoUI()
    {

    }

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
            dialogueUIActive = false;
        }
        else
        {
            uiLinks.dialogueUI.SetActive(true);
            dialogueUIActive = true;
        }
        
    }

    public void CloseDialogueUI()
    {
        uiLinks.dialogueUI.SetActive(false);
        dialogueUIActive = false;
    }

    public void OpenDialogueUI()
    {
        uiLinks.dialogueUI.SetActive(true);
        dialogueUIActive = true;
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

    // NOTIFICATION FUNCTIONS //

    public IEnumerator LaunchNotifyUI(string txt, float time)
    {
        uiLinks.notifyUIText.text = txt;
        uiLinks.notifyUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(time);

        uiLinks.notifyUIText.text = "NOTIFICATION";
        uiLinks.notifyUI.gameObject.SetActive(false);
    }


    //DEBUG FUNCTIONS //

    //Temporary HealthBar for an Ennemi
    

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
