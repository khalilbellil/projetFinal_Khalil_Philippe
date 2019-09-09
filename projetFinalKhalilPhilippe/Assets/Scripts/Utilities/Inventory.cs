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
                Debug.Log("Item was added to the inventory");
                UIManager.Instance.LaunchNotifyUI("You've picked up an object", 2);
            }
            else
            {
                UIManager.Instance.LaunchNotifyUI("Inventory is full !", 4);
                Debug.Log("Inventory is full");
            }
           
        }
        else
        {
            //add item in the same slot that the existing one (and increment the nb of units of this item)
            Debug.Log("Inventory already contains this item");
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        myItems.Remove(itemToRemove);
        Debug.Log("Item was removed from iventory");
        UIManager.Instance.LaunchNotifyUI("You've droped an object", 2);
    }

}
