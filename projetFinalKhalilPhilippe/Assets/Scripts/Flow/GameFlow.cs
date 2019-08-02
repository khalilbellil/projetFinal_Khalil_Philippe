using UnityEngine;

public class GameFlow : Flow
{
    MainEntry mainEntry;

    public static bool isGame;

    public override void Initialize()
    {
        mainEntry = GameObject.FindObjectOfType<MainEntry>();
        PlayerManager.Instance.Initialize(this);
        InputManager.Instance.Initialize();
    }

    public override void Update(float dt)
    {
        Debug.Log("GAMEFLOW UPDATE");
        InputManager.Instance.UpdateManager();
        PlayerManager.Instance.UpdateManager(dt);
    }

    public override void FixedUpdate(float dt)
    {
        InputManager.Instance.FixedUpdateManager();
        PlayerManager.Instance.FixedUpdateManager(dt);
    }

    public override void EndFlow()
    {

    }


    public void PlayerDied()
    {

    }
}
