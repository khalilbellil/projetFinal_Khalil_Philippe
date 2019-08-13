using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "QuestAsset", menuName = "MyScriptable/Quest")]
public class Quest : ScriptableObject
{

    public bool isAchieved;
    public int id;
    public string questName;
    public string description;
    public int requiredLvl;
   // public QuestType questType;

}
