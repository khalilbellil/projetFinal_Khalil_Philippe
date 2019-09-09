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
                itemToAdd.gameObject.SetActive(false);
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

    public void DropItem(SlotUI slot)
    {

        Item removedItem = slot.storedItem;
        Vector3 newPos = PlayerManager.Instance.player.transform.position + PlayerManager.Instance.player.transform.forward;
        removedItem.transform.position = new Vector3(newPos.x, .5f, newPos.z);
        removedItem.transform.rotation = PlayerManager.Instance.player.transform.localRotation;
        removedItem.gameObject.SetActive(true);

        slot.storedItem.addedToUI = false;
        slot.empty = true;
        PlayerManager.Instance.player.inventory.RemoveItem(slot.storedItem);
        slot.slotImage.texture = null;
        slot.slotImage.gameObject.SetActive(false);
    }

}
