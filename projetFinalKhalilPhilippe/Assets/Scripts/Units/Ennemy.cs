﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : BaseUnit
{
    // BASIC FUNCTIONS //

    enum States
    {
        Wander, Chase, Attack
    }

    RaycastHit hit;
    RaycastHit player;
    NavMeshAgent agent;
    Transform target;
    States currentAction;
    Vector3 wanderPos;
    public float sightRange;
    public LayerMask sightLayer;

    public void EnnemyInit()
    {
        base.Init();
        Debug.Log("Ennemy Init");
        agent = GetComponent<NavMeshAgent>();
    }

    public void EnnemyUpdate()
    {
        base.UnitUpdate();
    }

    public void EnnemyFixedUpdate()
    {
        base.UnitFixedUpdate();
        Debug.Log(wanderPos);

        if(currentAction == States.Wander && transform.position == wanderPos || currentAction == States.Wander && wanderPos == new Vector3())
        {
            wanderPos = (Random.insideUnitSphere + transform.position) * 10;
            wanderPos.y = transform.position.y;
        }


        if(target != null && transform.position*range == target.transform.position)
        {
            Debug.Log("dammit");
            currentAction = States.Attack;
        }
        Debug.Log(currentAction);

        switch (currentAction)
        {
            case States.Attack:
                UseWeapon(transform.forward);
                break;
            case States.Chase:
                UpdateSight();
                UpdateMovement(target.position);
                break;

            case States.Wander:
                UpdateSight();
                UpdateMovement(wanderPos);
                break;
        }
    }

    public override void UpdateMovement(Vector3 dir)
    {
        Debug.Log("move");
        agent.SetDestination(dir);
    }

    public void UpdateSight()
    {
        Vector3 rayDir = (target) ? (target.position - transform.position).normalized : transform.forward;

        if (Physics.Raycast(transform.position, rayDir, out hit, sightRange, sightLayer) && (hitableLayer == (hitableLayer | (1 << hit.transform.gameObject.layer))))
        {

            target = hit.transform;
            currentAction = States.Chase;
        }
        else
        {
            target = null;
            currentAction = States.Wander;
        }

    }

}
