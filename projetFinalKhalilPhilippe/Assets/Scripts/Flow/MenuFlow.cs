using UnityEngine;
using UnityEngine.UI;

public class MenuFlow : Flow
{
    Canvas menuCanvas;
    Button playButton, exitButton;
    MainEntry mainEntry;
    MenuUILinks menuUILinks;

    public override void Initialize()
    {
        mainEntry = GameObject.FindObjectOfType<MainEntry>();
        menuUILinks = GameObject.FindObjectOfType<MenuUILinks>();
        menuUILinks.playButton.onClick.AddListener(PlayButton);
        menuUILinks.exitButton.onClick.AddListener(ExitButton);
    }

    public override void Update(float dt)
    {
        Debug.Log("MENUFLOW Update");
    }

    public override void FixedUpdate(float dt)
    {

    }

    public override void EndFlow()
    {
        base.EndFlow();
    }

    public void PlayButton()
    {
        Debug.Log("PLAY");
        mainEntry.GoToNextFlow(CurrentState.Menu);//Switch to Game Scene/Flow.
    }


    public void ExitButton()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

}
