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
    [Header("Passive:")]
    public float multiplySpeedBy;
    public float multiplyDmgBy;
    public float maxHealthToAdd;
    [Header("Usable:")]
    public float healthToAdd;

    bool maxHealthWasChanged;
    bool speedWasChanged;
    bool dmgWasChanged;

    public bool addedToUI;

    public void ActivateEffects()
    {
        switch (itemType)
        {
            case ItemType.Usable:
                PlayerManager.Instance.player.health += healthToAdd;
                break;

            case ItemType.Passive:
                if (maxHealthToAdd != 0)
                {
                    PlayerManager.Instance.player.maxHealth += maxHealthToAdd;
                    PlayerManager.Instance.player.health = PlayerManager.Instance.player.maxHealth;
                    maxHealthWasChanged = true;
                }
                if (multiplyDmgBy != 0)
                {
                    PlayerManager.Instance.player.dmg *= multiplyDmgBy;
                    dmgWasChanged = true;
                }
                if (multiplySpeedBy != 0)
                {
                    PlayerManager.Instance.player.speed *= multiplySpeedBy;
                    speedWasChanged = true;
                }
                break;

            default:
                break;
        }
    }

    public void DesactivateEffects()
    {
        switch (itemType)
        {
            case ItemType.Usable:
                break;

            case ItemType.Passive:
                if (maxHealthWasChanged)
                {
                    PlayerManager.Instance.player.maxHealth -= maxHealthToAdd;
                    if (PlayerManager.Instance.player.health > PlayerManager.Instance.player.maxHealth)
                    {
                        PlayerManager.Instance.player.health = PlayerManager.Instance.player.maxHealth;
                    }
                }
                if (dmgWasChanged)
                {
                    PlayerManager.Instance.player.dmg /= multiplyDmgBy;
                }
                if (speedWasChanged)
                {
                    PlayerManager.Instance.player.speed /= multiplySpeedBy;
                }
                break;

            default:
                break;
        }
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
