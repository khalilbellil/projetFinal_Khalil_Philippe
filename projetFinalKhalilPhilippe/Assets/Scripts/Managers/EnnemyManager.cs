using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    HashSet<Ennemy> seePlayer;
    Transform _target;

    public void Initialize()
    {
        SpawnEnnemy();
        seePlayer = new HashSet<Ennemy>();
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
        ennemy = GameObject.FindObjectsOfType<Ennemy>().ToList();
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

    public void CleanUpQuestEnemies()
    {
        Debug.Log("Cleanup quest enemies");
        //delete the remaining enmies if exist
    }

    public void SeeTarget(Transform target , Ennemy e)
    {
        if(seePlayer.Count == 0)
        {
            foreach (Ennemy en in ennemy)
            {
               en.target = target;               
            }
        }


        if (!seePlayer.Contains(e))
        {
            _target = target;
            seePlayer.Add(e);
        }
    }

    public void LostTarget(Ennemy e)
    {
        _target = null;
        seePlayer.Remove(e);
        if(seePlayer.Count == 0)
            for (int i = 0; i < ennemy.Count; i++)
            {
                ennemy[i].setStates(Ennemy.States.Chase);
            }

    }

}
