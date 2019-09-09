﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILinks : MonoBehaviour
{
    [Header("Quests UI :")]
    public GameObject QuestsUI;
    public Text titleText;
    public Text activeQuestsTitleText;
    public Text quest1Title;
    public Text quest1Text;
    public Text quest2Title;
    public Text quest2Text;
    public Text quest3Title;
    public Text quest3Text;

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

    [Header("Inventory")]
    public GameObject inventoryUI;
    public Text rangeValue;
    public Text dammageValue;
    public Text speedValue;
    public Text maxHealthValue;

    [Header("Others :")]
    public GameObject pressKeyUI;
    public GameObject yesOrNoUI;
    public Text yesOrNoText;

}
