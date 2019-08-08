using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{

    #region Singleton Pattern
    private static UIManager instance = null;
    private UIManager() { }
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }
    #endregion

    UILinks uiLinks;
    // Start is called before the first frame update
    public void Init()
    {
        uiLinks = GameObject.FindObjectOfType<UILinks>();
    }

    // Update is called once per frame
    public void UpdateManager()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerManager.Instance.player.TakeDamage(10);
            Debug.Log("new hp amount: " + PlayerManager.Instance.player.health);
        }
        float a = (float)PlayerManager.Instance.player.health / PlayerManager.Instance.player.maxHealth;




        //Debug.Log("a: " + a);

        //changes the health bar color from green to red
        uiLinks.healthBar.fillAmount = a;
    }
}
