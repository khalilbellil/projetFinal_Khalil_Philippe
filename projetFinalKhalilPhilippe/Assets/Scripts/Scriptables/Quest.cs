using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "QuestAsset", menuName = "MyScriptable/Quest")]
public class Quest : ScriptableObject
{

    public bool isAchieved;
    public string questName;
    public string description;
    public int requiredLvl;
    public QuestType questType;

    [Header("ToDo list to achieve the quest:")]
    public string talkTo;
    [HideInInspector]
    public  bool talkToDone = false;
    public string pickUpItem;
    [HideInInspector]
    bool pickUpItemDone = false;

}
