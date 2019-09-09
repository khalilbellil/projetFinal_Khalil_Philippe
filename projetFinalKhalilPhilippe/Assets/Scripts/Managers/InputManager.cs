using UnityEngine;
using Rewired;

public class InputManager
{
    #region Singleton Pattern
    private static InputManager instance = null;
    private InputManager() { }
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InputManager();
            }
            return instance;
        }
    }
    #endregion

    public InputPkg fixedInputPressed; //Every fixed update we fill this
    public InputPkg inputPressed;      //Every update we fill this
    Rewired.Player rewiredPlayer;
    int playerId = 0;

    public void Initialize()
    {
        rewiredPlayer = ReInput.players.GetPlayer(playerId);
        rewiredPlayer.controllers.hasMouse = true;
        Cursor.lockState = CursorLockMode.Locked;
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        fixedInputPressed = new InputPkg();
        inputPressed = new InputPkg();
    }

    void OnControllerConnected(ControllerStatusChangedEventArgs args)
    {
        rewiredPlayer.controllers.hasMouse = false;
        //Debug.Log("yeet");
    }

    public void UpdateManager()
    {
        //Mouse inputs:
        inputPressed.deltaMouse.x = rewiredPlayer.GetAxis("Move Camera");
        inputPressed.mousePosToRay = inputPressed.MousePosToRay(Input.mousePosition);
        inputPressed.leftMouseButtonPressed = rewiredPlayer.GetButtonDown("Attack");
        inputPressed.rightMouseButtonPressed = rewiredPlayer.GetButtonDown("Ability");
        inputPressed.middleMouseButtonPressed = Input.GetMouseButton(2);

        //Movement inputs:
        inputPressed.dirPressed.x = rewiredPlayer.GetAxis("Move Horizontal");
        inputPressed.dirPressed.y = rewiredPlayer.GetAxis("Move Vertical");
        inputPressed.jumpPressed = rewiredPlayer.GetButtonDown("Jump");

        //Interactions inputs:
        inputPressed.anyKeyPressed = Input.anyKeyDown;
        inputPressed.inventoryPressed = rewiredPlayer.GetButtonDown("Inventory");
        inputPressed.interactPressed = rewiredPlayer.GetButtonDown("Interaction");
        inputPressed.previousSpellPressed = Input.GetButtonDown("Previous Spell");
        inputPressed.nextSpellPressed = Input.GetButtonDown("Next Spell");
        inputPressed.questsPressed = rewiredPlayer.GetButtonDown("OpenQuests");
        inputPressed.openDialogueTempPressed = rewiredPlayer.GetButtonDown("OpenDialogueTemp");
    }

    public void FixedUpdateManager()
    {
        //Mouse inputs:
        fixedInputPressed.deltaMouse.x = rewiredPlayer.GetAxis("Move Camera");
        fixedInputPressed.mousePosToRay = inputPressed.MousePosToRay(Input.mousePosition);
        fixedInputPressed.leftMouseButtonPressed = rewiredPlayer.GetButtonDown("Attack");
        fixedInputPressed.rightMouseButtonPressed = rewiredPlayer.GetButtonDown("Attack");
        fixedInputPressed.middleMouseButtonPressed = Input.GetMouseButton(2);

        //Movement inputs:
        fixedInputPressed.dirPressed.x = rewiredPlayer.GetAxis("Move Horizontal");
        fixedInputPressed.dirPressed.y = rewiredPlayer.GetAxis("Move Vertical");
        fixedInputPressed.jumpPressed = rewiredPlayer.GetButtonDown("Jump");
    }

    public void StopManager()
    {

    }

    public class InputPkg
    {
        //Mouse inputs:
        public Vector2 deltaMouse;   //the delta change of mouse position
        public Ray mousePosToRay;
        public bool leftMouseButtonPressed;
        public bool rightMouseButtonPressed;
        public bool middleMouseButtonPressed;
        public Vector3 mousePos;

        //Movements inputs:
        public Vector3 dirPressed;   //side to side and foward and back
        public bool jumpPressed;  //If jump was pressed this frame

        //Interaction inputs:
        public bool anyKeyPressed;
        public bool inventoryPressed;
        public bool interactPressed;
        public bool questsPressed;
        public bool previousSpellPressed;
        public bool nextSpellPressed;
        public bool openDialogueTempPressed;



        public Ray MousePosToRay(Vector3 mousePos)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            return ray;
        }

        public override string ToString()
        {
            return string.Format("DirPressed[{0}],DeltaMouse[{1}],JumpPressed[{2}]", dirPressed, deltaMouse, jumpPressed);
        }

    }

}
