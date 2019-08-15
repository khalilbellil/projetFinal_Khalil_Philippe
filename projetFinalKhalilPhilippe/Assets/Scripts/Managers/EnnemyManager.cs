using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyManager
{
    #region Singleton Pattern
    private static EnnemyManager instance = null;
    private EnnemyManager() { }
    public static EnnemyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnnemyManager();
            }
            return instance;
        }
    }
    #endregion

    public Ennemy ennemy;

    public void Initialize()
    {
        SpawnEnnemy();
    }

    public void UpdateManager(float dt)
    {
        if (ennemy != null)
        {
            ennemy.EnnemyUpdate();
        }
    }

    public void FixedUpdateManager(float dt)
    {
        ennemy.EnnemyFixedUpdate();
    }

    public void StopManager()
    {//Reset everything
        instance = null;
    }

    public void SpawnEnnemy()
    {
        ennemy = GameObject.Find("Ennemy").GetComponent<Ennemy>();
        ennemy.EnnemyInit();
    }

}
