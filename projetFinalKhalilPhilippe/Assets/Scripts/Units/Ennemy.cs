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
    public Transform target;
    Transform[] walkpatern;
    Vector3 startPos;
    int walkPaternIndex = 0;
    public Transform walk;
    States currentAction;
    Vector3 wanderPos;
    public float sightRange;
    public LayerMask sightLayer;
    Animator anim;

    public void EnnemyInit()
    {
        base.Init();
        Debug.Log("Ennemy Init");
        agent = GetComponent<NavMeshAgent>();
        InitializeWalkPatern();
        startPos = transform.position;
        anim = GetComponent<Animator>();
    }

    public void EnnemyUpdate()
    {
        if (isAlive)
        {
            base.UnitUpdate();
            TestEnnemiHpBar();

        }
    }

    public void EnnemyFixedUpdate()
    {
        //Debug.Log(wanderPos);
        if (isAlive)
        {

            base.UnitFixedUpdate();
            if (target != null && Vector3.Distance(transform.position, target.transform.position) <= range)
            {
                //Debug.Log("dammit");
                currentAction = States.Attack;
            }

            if(unitName == "1")
            Debug.Log(currentAction);
            UpdateAnims();

            switch (currentAction)
            {
                case States.Attack:
                    transform.LookAt(target);
                    UseWeapon(transform.forward);
                    currentSpeed = 0;
                    anim.SetTrigger("AttackTrigger");
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
    }

    public override void UpdateMovement(Vector3 goalPos)
    {
        //Debug.Log("move");
        //Debug.Log(((transform.position - dir).normalized * range) + transform.position);
        agent.SetDestination(goalPos);
        if (!isAlive)
        {
            agent.SetDestination(transform.position);
            currentSpeed = 0;
        }
        else
        {
            if (unitName == "1")
                Debug.Log("b");
            currentSpeed = speed;
        }
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

    void UpdateAnims()
    {
        if (unitName == "1")
            Debug.Log(currentSpeed);
        anim.SetFloat("forward", currentSpeed);
    }

    Vector3 WalkToBalise()
    {
        Vector3 wtf = walkpatern[walkPaternIndex].position;
        wtf.y = transform.position.y;
        if (Vector3.Distance(transform.position, wtf) <= 0.1)
        {
            if (walkPaternIndex < walk.childCount - 1)
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

    public override void Death()
    {
        base.Death();
        anim.SetTrigger("DeathTrigger");
    }
}
