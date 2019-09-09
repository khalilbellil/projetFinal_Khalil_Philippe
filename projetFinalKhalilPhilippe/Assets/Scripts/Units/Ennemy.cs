using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : BaseUnit
{
    // BASIC FUNCTIONS //

    public enum States
    {
        Wander, Chase, Attack ,Find
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
    float lockOnRange;
    public LayerMask sightLayer;
    Animator anim;

    public void EnnemyInit()
    {
        base.Init();
        lockOnRange = sightRange * 3;
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

            if(target != null && currentAction != States.Chase && currentAction != States.Attack)
            {
                if(unitName == "1")
                    Debug.Log("was in mode: " + currentAction);
                currentAction = States.Find;
            }

            if(unitName == "1")
            Debug.Log(currentAction);
            UpdateAnims();
            switch (currentAction)
            {
                case States.Attack:
                    transform.LookAt(target);
                    anim.SetTrigger("AttackTrigger");
                    UseWeapon(transform.forward);
                    currentSpeed = 0;
                    UpdateSight();
                    break;
                case States.Chase:
                    UpdateMovement(((transform.position - target.position).normalized * (range - .02f)) + target.position);
                    UpdateSight();
                    break;

                case States.Wander:
                    UpdateMovement(WalkToBalise());
                    UpdateSight();
                    break;

                case States.Find:
                    UpdateMovement(((transform.position - target.position).normalized * (range - .02f)) + target.position);
                    break;
            }
           

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
            currentSpeed = speed;
        }
    }

    bool SensesCheck()
    {
        if (unitName == "1" && States.Chase == currentAction)
            Debug.Log("Sense check, target: " + target + ", in chase");
        Vector3 rayDir = (PlayerManager.Instance.player.transform.position - transform.position).normalized;// : transform.forward;
        //float Iseerange = (target) ? sightRange : lockOnRange;

        Collider[] temp = Physics.OverlapSphere(transform.position, 3, hitableLayer);
        if (temp.Length > 0)
        {
            target = temp[0].transform;
            EnnemyManager.Instance.SeeTarget(target, this);
            return true;
        }


        if (!target)
        {
            Vector2 pV2 = new Vector2(PlayerManager.Instance.player.transform.position.x, PlayerManager.Instance.player.transform.position.z);
            Vector2 eV2 = new Vector2(transform.position.x, transform.position.z);
            float angToPlayer = Vector2.Angle(transform.forward,(eV2-pV2).normalized);
            if (unitName == "1")
                Debug.Log(angToPlayer);
            if (angToPlayer> 140)
                if (Physics.Raycast(transform.position, rayDir, out hit, sightRange, sightLayer) && (hitableLayer == (hitableLayer | (1 << hit.transform.gameObject.layer))))
                {
                    target = hit.transform;
                    EnnemyManager.Instance.SeeTarget(target, this);
                    return true;
                }
            return false;
        }
        else
        {
            Physics.Raycast(transform.position, rayDir, out hit, lockOnRange, LayerMask.GetMask("Obstacle", "Player"));
            if (Vector2.Distance(target.position, transform.position) > lockOnRange || (hit.transform && hit.transform.tag != "Player"))
            {
                target = null;
                EnnemyManager.Instance.LostTarget(this);
                return false;
            }
            return true;   
        }
        
    }

    public void UpdateSight()
    {
        if (SensesCheck())
        {
            
                currentAction = States.Chase;
        }
        else
        {
            target = null;
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
        QuestManager.Instance.NotifyQuestThatEnnemyWasKilled();
        base.Death();
        EnnemyManager.Instance.LostTarget(this);
        agent.SetDestination(transform.position);
        anim.SetTrigger("DeathTrigger");
        //OnDeathEventHandler?.Invoke(); //notify

        
    }

    public void setStates(States s)
    {
        currentAction = s;
        if (s == States.Chase && target == null)
            currentAction = States.Wander;
    }
}
