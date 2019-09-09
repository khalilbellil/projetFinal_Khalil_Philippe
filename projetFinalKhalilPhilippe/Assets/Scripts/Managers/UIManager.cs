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
    public MainEntry mainEntry;
    public List<SlotUI> mySlots;

    public bool dialogueUIActive;

    public void Initialize()
    {
        mainEntry = GameObject.FindObjectOfType<MainEntry>();
        uiLinks = GameObject.FindObjectOfType<UILinks>();
        uiLinks.acceptButton.onClick.AddListener(QuestManager.Instance.AcceptQuest);
        uiLinks.declineButton.onClick.AddListener(QuestManager.Instance.DeclineQuest);
        uiLinks.restartButton.onClick.AddListener(mainEntry.RestartGame);
        uiLinks.exitButton.onClick.AddListener(mainEntry.ExitGame);
        mySlots = uiLinks.slotsUI;
        foreach (SlotUI slotUI in mySlots)
        {
            slotUI.button.onClick.AddListener(() => { PlayerManager.Instance.player.inventory.DropItem(slotUI); });
        }
    }

    public void UpdateManager()
    {
        UpdateHealthBarUI();
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

    // GAMEOVER FUNCTIONS //

    public void OpenGameOverUI(string _gameOverText)
    {
        uiLinks.gameOverText.text = _gameOverText;
        uiLinks.gameOverUI.SetActive(true);
        UnlockMouse();
    }


    // INVENTORY FUNCTIONS //

    public void OpenCloseInventoryUI()
    {
        if (InputManager.Instance.inputPressed.inventoryPressed)
        {
            if (!uiLinks.inventoryUI.activeSelf)
            {
                SetStatsValuesInventoryUI(PlayerManager.Instance.player.range, PlayerManager.Instance.player.dmg, PlayerManager.Instance.player.speed, PlayerManager.Instance.player.maxHealth, PlayerManager.Instance.player.health);
                LoadInventoryItemsUI();
                uiLinks.inventoryUI.SetActive(true);
                UnlockMouse();
            }
            else
            {
                uiLinks.inventoryUI.SetActive(false);
                LockMouse();
            }
        }
    }

    public void SetStatsValuesInventoryUI(float range, float dammage, float speed, float maxHealth, float health)
    {
        uiLinks.rangeValue.text = range.ToString();
        uiLinks.dammageValue.text = dammage.ToString();
        uiLinks.speedValue.text = speed.ToString();
        uiLinks.maxHealthValue.text = maxHealth.ToString();
        uiLinks.healthValue.text = health.ToString();
    }

    public void LoadInventoryItemsUI()
    {
        foreach (Item item in PlayerManager.Instance.player.inventory.myItems)
        {
            foreach (SlotUI slotUI in mySlots)
            {
                if (slotUI.empty == true && item.addedToUI == false)
                {
                    item.addedToUI = true;
                    slotUI.empty = false;
                    slotUI.slotImage.gameObject.SetActive(true);
                    slotUI.slotImage.texture = item.slotImage.texture;
                    slotUI.storedItem = item;

                }
            }
        }
    }



    // QUESTS FUNCTIONS //

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
            uiLinks.questText.text = QuestManager.Instance.activeQuest.questName + ":  " + QuestManager.Instance.activeQuest.description;
        }
        if ((QuestManager.Instance.myAchivedQuests.Count != 0))
        {
            uiLinks.achivedQuestsText.text = QuestManager.Instance.myAchivedQuests[0].questName + "  //  ";
            for (int i = 1; i < QuestManager.Instance.myAchivedQuests.Count; i++)
            {
                uiLinks.achivedQuestsText.text += QuestManager.Instance.myAchivedQuests[i].questName + "  //  ";
            }
        }
    }


    //DIALOGUE FUNCTIONS //

    public void OpenYesOrNoUI(string question)
    {
        uiLinks.yesOrNoText.text = question;
        uiLinks.yesOrNoUI.SetActive(true);
        UnlockMouse();
    }

    public void CloseYesOrNoUI()
    {
        uiLinks.yesOrNoUI.SetActive(false);
        LockMouse();
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


    //MOUSE FUNCTIONS //

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    //DEBUG FUNCTIONS //

    void UpdateHealthBarUI()
    {
        float a = (float)PlayerManager.Instance.player.health / PlayerManager.Instance.player.maxHealth;
        uiLinks.healthBar.fillAmount = a;
    }
    
}
