using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Usable, Passive
}

public class Item : MonoBehaviour
{
    public int id;
    public string itemName;
    public string description;
    public ItemType itemType;
    public float multiplySpeedBy;
    public float multiplyDmgBy;
    public float healthToAdd;
    public float maxHealthToAdd;
}
