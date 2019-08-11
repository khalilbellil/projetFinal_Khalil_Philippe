using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    #region Singleton Pattern
    private static PlayerManager instance = null;
    private PlayerManager() { }
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerManager();
            }
            return instance;
        }
    }
    #endregion

    public Player player;

    public void Initialize()
    {
        SpawnPlayer();
    }

    public void UpdateManager(float dt)
    {
        if (player != null)
        {
            player.PlayerUpdate();
        }
    }

    public void FixedUpdateManager(float dt)
    {
        player.PlayerFixedUpdate();

    }

    public void StopManager()
    {//Reset everything
        instance = null;
    }


    void SpawnPlayer()
    {
        //Instantiate the Player(s)
        player = GameObject.FindObjectOfType<Player>();
        player.PlayerInit();
    }

}
