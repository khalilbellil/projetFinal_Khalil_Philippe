using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager
{

    #region Singleton Pattern
    private static DialogueManager instance = null;
    private DialogueManager() { }
    public static DialogueManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DialogueManager();
            }
            return instance;
        }
    }
    #endregion

    

}
