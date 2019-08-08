using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : MonoBehaviour
{
    public string pnjName;
    public Quest myQuest;

    private void Start()
    {
        if (pnjName == "")
        {
            pnjName = "Name not setted !";
        } //verification

    }

}
