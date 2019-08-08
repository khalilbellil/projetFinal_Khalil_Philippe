using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : BaseUnit
{
    // BASIC FUNCTIONS //

    public void EnnemyInit()
    {
        base.Init();
        Debug.Log("Ennemy Init");
    }

    public void EnnemyUpdate()
    {
        base.UnitUpdate();
    }

    public void EnnemyFixedUpdate()
    {
        base.UnitFixedUpdate();
    }



}
