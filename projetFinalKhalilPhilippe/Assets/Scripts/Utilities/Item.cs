using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Usable, Passive
}

public class Item : MonoBehaviour
{
    public Sprite slotImage;
    public string prefabDir;
    public int id;
    public string itemName;
    public string description;
    public ItemType itemType;
    public float multiplySpeedBy;
    public float multiplyDmgBy;
    public float healthToAdd;
    public float maxHealthToAdd;

    public bool addedToUI;

    public void ActivateEffects()
    {

    }

    public void DesactivateEffects()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player p = other.GetComponent<Player>();
            p.target = this.gameObject;
            UIManager.Instance.OpenPressKeyUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ClosePressKeyUI();
            other.GetComponent<Player>().target = null;
        }
    }

}
