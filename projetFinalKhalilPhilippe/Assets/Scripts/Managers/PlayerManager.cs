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
    public GameFlow gameFlow;

    public void Initialize(GameFlow gameFlow)
    {
        this.gameFlow = gameFlow;
        player = GameObject.Find("Player").GetComponent<Player>();
        player.Init();

        //Debug.Log("PlayerManager Initialize");

    }

    public void UpdateManager(float dt)
    {
        //input.UpdateActions(InputManager.Instance.inputPressed.dirPressed, player);
        if (player != null)
        {
            player.PlayerUpdate(InputManager.Instance.inputPressed);
        }
    }

    public void FixedUpdateManager(float dt)
    {
        //input.UpdatePosition(InputManager.Instance.inputPressed.dirPressed, player);
        player.PlayerFixedUpdate(InputManager.Instance.fixedInputPressed);

    }

    public void StopManager()
    {//Reset everything
        instance = null;
    }


    void SpawnPlayer()
    {
        //Instantiate the Player(s)
        //player = GameObject.Instantiate(Resources.Load<Player>(PrefabsDir.playerDir));
        //player.enabled = true;
    }

    void GameOver()
    {
        //player.Death();
        //Game Over UI Here (Call the UIManager function to activate the concerned UI)
        Debug.Log("Game Over !");

    }

}
