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

    public Ennemy[] ennemy;

    public void Initialize()
    {
        SpawnEnnemy();
    }

    public void UpdateManager(float dt)
    {
        if (ennemy != null)
        {
            foreach (Ennemy e in ennemy)
            {
                e.EnnemyUpdate();
            }
        }
    }

    public void FixedUpdateManager(float dt)
    {
        foreach (Ennemy e in ennemy)
        {
            e.EnnemyFixedUpdate();
        }
    }

    public void StopManager()
    {//Reset everything
        instance = null;
    }

    public void SpawnEnnemy()
    {
        ennemy = GameObject.FindObjectsOfType<Ennemy>();
        foreach (Ennemy e in ennemy)
        {
            e.EnnemyInit();
        }
    }

}
