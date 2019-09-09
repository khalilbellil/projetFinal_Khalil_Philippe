using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public int slotNb;
    public List<Item> myItems;

    private void Start()
    {
        myItems = new List<Item>();
    }

    public void AddItem(Item itemToAdd)
    {
        if (!myItems.Contains(itemToAdd))
        {
            if (myItems.Count < slotNb)
            {
                myItems.Add(itemToAdd);
                UIManager.Instance.LaunchNotifyUI("You've picked up an item", 2);
            }
            else
            {
                UIManager.Instance.LaunchNotifyUI("Inventory is full !", 4);
            }
           
        }
        else
        {
            //add item in the same slot that the existing one (and increment the nb of units of this item)
            UIManager.Instance.LaunchNotifyUI("Inventory already contains this item !", 4);
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        myItems.Remove(itemToRemove);
        UIManager.Instance.LaunchNotifyUI("You've droped an item", 2);
    }

}
