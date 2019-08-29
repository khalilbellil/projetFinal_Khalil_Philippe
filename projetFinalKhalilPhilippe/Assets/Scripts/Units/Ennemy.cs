using System.Collections;
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
    Transform[] walkpatern;
    int walkPaternIndex = 0;
    public Transform walk;
    States currentAction;
    Vector3 wanderPos;
    public float sightRange;
    public LayerMask sightLayer;

    public void EnnemyInit()
    {
        base.Init();
        Debug.Log("Ennemy Init");
        agent = GetComponent<NavMeshAgent>();
        InitializeWalkPatern();
    }

    public void EnnemyUpdate()
    {
        base.UnitUpdate();
        TestEnnemiHpBar();
    }

    public void EnnemyFixedUpdate()
    {
        base.UnitFixedUpdate();
        Debug.Log(wanderPos);

        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= range)
        {
            //Debug.Log("dammit");
            currentAction = States.Attack;
        }

        Debug.Log(currentAction);

        switch (currentAction)
        {
            case States.Attack:
                UseWeapon(transform.forward);
                break;
            case States.Chase:
                UpdateMovement(((transform.position - target.position).normalized * (range - .02f)) + target.position);
                break;

            case States.Wander:
                UpdateMovement(WalkToBalise());
                break;
        }
        UpdateSight();
    }

    public override void UpdateMovement(Vector3 dir)
    {
        //Debug.Log("move");
        //Debug.Log(((transform.position - dir).normalized * range) + transform.position);
        agent.SetDestination(dir);
    }

    bool SensesCheck()
    {
        Vector3 rayDir = (target) ? (target.position - transform.position).normalized : transform.forward;

        if (Physics.Raycast(transform.position, rayDir, out hit, sightRange, sightLayer) && (hitableLayer == (hitableLayer | (1 << hit.transform.gameObject.layer))))
        {
            target = hit.transform;
            return true;
        }

        Collider[] temp = Physics.OverlapSphere(transform.position, 3, hitableLayer);
        if (temp.Length > 0)
        {
            target = temp[0].transform;
            return true;
        }

        return false;
    }

    public void UpdateSight()
    {
        Vector3 rayDir = (target) ? (target.position - transform.position).normalized : transform.forward;

        if (SensesCheck())
        {
            currentAction = States.Chase;
        }
        else
        {
            target = null;
            if (currentAction == States.Chase)
            {
                Debug.Log("yolo");
            }

            currentAction = States.Wander;
        }

    }

    void InitializeWalkPatern()
    {
        walkpatern = new Transform[walk.childCount];
        for (int i = 0; i < walk.childCount; i++)
        {
            walkpatern[i] = walk.GetChild(i);
        }
    }

    Vector3 WalkToBalise()
    {
        Vector3 wtf = walkpatern[walkPaternIndex].position;
        wtf.y = transform.position.y;
        if (Vector3.Distance(transform.position,wtf) <= 0.1)
        {
            if (walkPaternIndex < walk.childCount-1)
            {
                walkPaternIndex++;
            }
            else
            {
                walkPaternIndex = 0;
            }
        }

        wanderPos = walkpatern[walkPaternIndex].position;
        wanderPos.y = transform.position.y;

        return wanderPos;
    }

    void TestEnnemiHpBar()
    {
        float hpLeft = health / maxHealth;
        transform.GetChild(0).localScale = new Vector3(hpLeft, 1, 1);
    }
}
