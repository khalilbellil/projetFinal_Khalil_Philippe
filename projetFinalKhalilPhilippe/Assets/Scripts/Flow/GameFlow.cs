using UnityEngine;

public class GameFlow : Flow
{
    MainEntry mainEntry;

    public static bool isGame;

    public override void Initialize()
    {
        mainEntry = GameObject.FindObjectOfType<MainEntry>();
    }

    public override void Update(float dt)
    {
        Debug.Log("GAMEFLOW UPDATE");
    }

    public override void FixedUpdate(float dt)
    {

    }

    public override void EndFlow()
    {

    }


    public void PlayerDied()
    {

    }
}
