using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILinks : MonoBehaviour
{
    [Header("Quests UI :")]
    public GameObject QuestsUI;
    public Text questText;
    public Text achivedQuestsText;

    [Header("PNJ UI :")]
    public GameObject dialogueUI;
    public Text dialogueText, PnjNameText;

    [Header("Player Stats UI :")]
    public Image healthBar;
    public Image ManaBar;
    public Image characterPic;
    public Image Ability;
    public Text targetUI;
    public Text notifyUIText;
    public Image notifyUI;

    [Header("Inventory UI :")]
    public GameObject inventoryUI;
    public Text rangeValue;
    public Text dammageValue;
    public Text speedValue;
    public Text maxHealthValue;
    public Text healthValue;
    public List<SlotUI> slotsUI;

    [Header("GameOver UI :")]
    public GameObject gameOverUI;
    public Button restartButton;
    public Button exitButton;
    public Text gameOverText;

    [Header("Others :")]
    public GameObject pressKeyUI;
    public GameObject yesOrNoUI;
    public Button acceptButton;
    public Button declineButton;
    public Text yesOrNoText;

}
