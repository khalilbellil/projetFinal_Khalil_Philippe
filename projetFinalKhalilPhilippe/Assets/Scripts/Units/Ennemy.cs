using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : BaseUnit
{
    // BASIC FUNCTIONS //

    RaycastHit hit;
    NavMeshAgent agent;
    public float sightRange;
    public bool EnnemiFound { get { return Physics.Raycast(transform.position, transform.forward, out hit, sightRange, hitableLayer); } }

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
        if (EnnemiFound)
        {
            Debug.Log("gotcha");
            if (Vector3.Distance(transform.position, hit.transform.position) <= range)
            {
                UseWeapon(transform.forward);
            }
            else
            {
                UpdateMovement(PlayerManager.Instance.player.transform.position);
            }
        }
    }

    public override void UpdateMovement(Vector3 dir)
    {
        agent.SetDestination(dir);
    }

}
