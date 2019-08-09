using UnityEngine;


public class GameFlow : Flow
{
    MainEntry mainEntry;

    public static bool isGame;

    public override void Initialize()
    {
        mainEntry = GameObject.FindObjectOfType<MainEntry>();
        InputManager.Instance.Initialize();
        PlayerManager.Instance.Initialize();
        UIManager.Instance.Initialize();
        EnnemyManager.Instance.Initialize();
        QuestManager.Instance.Initialize();
    }

    public override void Update(float dt)
    {
        UIManager.Instance.UpdateManager();
        InputManager.Instance.UpdateManager();
        PlayerManager.Instance.UpdateManager(dt);
        EnnemyManager.Instance.UpdateManager(dt);
        QuestManager.Instance.UpdateManager();
    }

    public override void FixedUpdate(float dt)
    {
        InputManager.Instance.FixedUpdateManager();
        PlayerManager.Instance.FixedUpdateManager(dt);
        EnnemyManager.Instance.FixedUpdateManager(dt);
        QuestManager.Instance.FixedUpdateManager();
    }

    public override void EndFlow()
    {
        InputManager.Instance.StopManager();
        PlayerManager.Instance.StopManager();
        EnnemyManager.Instance.StopManager();
        QuestManager.Instance.StopManager();
    }


    public void PlayerDied()
    {

    }

}
