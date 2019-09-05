using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public List<Ennemy> ennemy;

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
        ennemy = GameObject.FindObjectsOfType<Ennemy>().ToList<Ennemy>();
        foreach (Ennemy e in ennemy)
        {
            e.EnnemyInit();
        }
    }

    public Ennemy[] SpawnEnnemiesCloseToPlayer(int num)
    {
        GameObject[] enemiesGo = new GameObject[num];
        Ennemy[] eToReturn = new Ennemy[num];
        for (int i = 0; i < num; i++)
        {
            enemiesGo[i] = GameObject.Instantiate(Resources.Load<GameObject>(PrefabsDir.bruteEnnemy)); //Instantiate Ennemies
            enemiesGo[i].transform.position = PlayerManager.Instance.player.transform.position + new Vector3(5, 0, 5); //Set position
            eToReturn[i] = enemiesGo[i].GetComponent<Ennemy>();
        }

        return eToReturn;
    }

    public void AddQuestEnemies(int num, TaskEventHandler funcToInvoke)
    {
        //spawn enemies
        Debug.Log("Quest ennemies spawned");

        Ennemy[] ennemiesToAdd = SpawnEnnemiesCloseToPlayer(num);
        for (int i = 0; i < num; i++)
        {
            ennemy.Add(ennemiesToAdd[i]); //add instantiated ennemies to the track list
            ennemy[i].OnDeathEventHandler += CleanUpQuestEnemies; //add to enemies onDeathEvent the funcToInvoke
        }

        

    }

    public void CleanUpQuestEnemies()
    {
        Debug.Log("Cleanup quest enemies");
        //delete the remaining enmies if exist
    }
    
}
