using UnityEngine;
using UnityEngine.UI;

public class MenuFlow : Flow
{
    Canvas menuCanvas;
    Button playButton, optionsButton, exitButton;
    MainEntry mainEntry;

    public override void Initialize()
    {

    }

    public override void Update(float dt)
    {

    }

    public override void FixedUpdate(float dt)
    {

    }

    public override void EndFlow()
    {

    }

    public void PlayButton()
    {
        Debug.Log("PLAY");
        mainEntry.GoToNextFlow(CurrentState.Menu);//Switch to Game Scene/Flow.
    }

    void OptionsButton()
    {
        Debug.Log("OPTIONS");
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("EXIT");
    }

}
