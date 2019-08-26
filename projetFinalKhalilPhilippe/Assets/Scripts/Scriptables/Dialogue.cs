using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dialogue : MonoBehaviour
{
    public List<string> dialogues;
    int actualDialogueIndex;

    private void Start()
    {
        actualDialogueIndex = 0;
    }

    private void Update()
    {
        
    }

    public void LaunchDialogue(PNJ pnj)
    {
        if (actualDialogueIndex < dialogues.Count)
        {
            UIManager.Instance.CreateDialogue(pnj.pnjName, dialogues[actualDialogueIndex]);
            actualDialogueIndex++;
        }
    }

}
