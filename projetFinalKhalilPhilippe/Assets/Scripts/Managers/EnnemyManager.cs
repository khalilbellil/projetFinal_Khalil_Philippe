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

    Ennemy testEnnemy;

    public void Initialize()
    {
        SpawnEnnemy();
    }

    public void UpdateManager(float dt)
    {
        if (testEnnemy != null)
        {
            testEnnemy.EnnemyUpdate();
        }
    }

    public void FixedUpdateManager(float dt)
    {
        testEnnemy.EnnemyFixedUpdate();
    }

    public void StopManager()
    {//Reset everything
        instance = null;
    }

    public void SpawnEnnemy()
    {
        testEnnemy = GameObject.Find("Ennemy").GetComponent<Ennemy>();
        testEnnemy.EnnemyInit();
    }

}
