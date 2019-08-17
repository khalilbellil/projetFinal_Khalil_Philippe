using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : MonoBehaviour
{
    public string pnjName;
    public Quest myQuest;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Activate interaction UI
        //if interaction pressed -> activate dialogue ui with the pnj quest -> And Desactivate interaction UI
    }

}
