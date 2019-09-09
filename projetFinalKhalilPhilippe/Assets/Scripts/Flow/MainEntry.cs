using UnityEngine;
using UnityEngine.SceneManagement;

public enum CurrentState { Game, Menu, End }

public class MainEntry : MonoBehaviour
{
    protected bool flowInitialized = false;
    CurrentState currentState;

    public Flow curFlow;
    public static int sceneNb = 1;

    public void Initialize(CurrentState cs)
    {
        //THIS IS THE FIRST POINT EVER ENTERED BY THIS PROGRAM. (Except for MainEntryCreator.cs, who creates this script and runs this function for the game to start)
        currentState = cs;
        curFlow = InitializeFlowScript(currentState, true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if (flowInitialized)
        {
            if (curFlow == null)
                return; //This means Initialize hasnt been called yet, can happen in weird Awake/Update way (should not though, but be safe)
            curFlow.Update(dt);
        }

    }

    private void FixedUpdate()
    {
        if (flowInitialized && curFlow != null)
        {
            float dt = Time.fixedDeltaTime;
            curFlow.FixedUpdate(dt);
        }

    }

    public Flow InitializeFlowScript(CurrentState flowType, bool sceneAlreadyLoaded)
    {
        Flow newFlow;
        switch (flowType)
        {
            case CurrentState.Game:
                newFlow = new GameFlow();
                break;
            case CurrentState.Menu:
                newFlow = new MenuFlow();
                break;
            case CurrentState.End:
                newFlow = new MenuFlow();
                break;
            default:
                Debug.Log("Flow could not be loaded " + flowType);
                return null;
        }

        if (!sceneAlreadyLoaded)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; //Clean any listener already on
            SceneManager.sceneLoaded += OnSceneLoaded; //Delay flow initialization until 
        }
        else
        {
            newFlow.Initialize();
            flowInitialized = true;
        }

        return newFlow;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Old scenes might still be listening, so doublecheck
        bool verified = false;
        switch (scene.name)
        {
            case "Game":
                verified = (currentState == CurrentState.Game);
                break;
            case "Menu":
                verified = (currentState == CurrentState.Menu);
                break;
            default:
                Debug.Log("Switch case not found: " + scene.name);
                break;
        }

        if (verified)
        {
            curFlow.Initialize();
            flowInitialized = true;
        }
        else
            Debug.Log("Unerror! Unverified scene!");
    }

    public void GoToNextFlow(CurrentState cs)
    {
        if (curFlow != null)
        {
            curFlow.EndFlow();
            flowInitialized = false;
        }
        //Assume Flow called Clean already
        //Load the next scene        
        switch (cs)
        {
            case CurrentState.Game:
                SceneManager.LoadScene("Scenes/Menu");
                cs = CurrentState.Menu;
                break;
            case CurrentState.Menu:
                SceneManager.LoadScene("Scenes/Game");
                cs = CurrentState.Game;
                break;
            default:
                Debug.LogError("Unhandled Switch: " + cs);
                return;
        }
        currentState = cs;
        //Initialize the flow script for the scene
        curFlow = InitializeFlowScript(cs, false);
    }

    public void RestartGame()
    {
        GoToNextFlow(CurrentState.Menu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
