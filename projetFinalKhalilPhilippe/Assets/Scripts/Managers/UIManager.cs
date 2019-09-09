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

    // GAMEOVER FUNCTIONS //

    public void OpenGameOverUI()
    {
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
                SetStatsValuesInventoryUI(PlayerManager.Instance.player.range, PlayerManager.Instance.player.dmg, PlayerManager.Instance.player.speed, PlayerManager.Instance.player.maxHealth);
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

    public void SetStatsValuesInventoryUI(float range, float dammage, float speed, float maxHealth)
    {
        uiLinks.rangeValue.text = range.ToString();
        uiLinks.dammageValue.text = dammage.ToString();
        uiLinks.speedValue.text = speed.ToString();
        uiLinks.maxHealthValue.text = maxHealth.ToString();
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
