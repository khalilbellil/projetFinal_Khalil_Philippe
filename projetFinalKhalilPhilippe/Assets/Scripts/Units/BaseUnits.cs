using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(Rigidbody))]
public class BaseUnit : MonoBehaviour
{
    //To add more weapons, increase the size of the weaponList in the unity, on the character's prefab
    //and pass it a prefab that is or inherits of Weapon class
    //public Weapon[] weaponList;

    #region VARIABLES
    public bool isAlive;
    protected bool isDashing;
    protected bool isHolding;
    private float dashTime;     //time for how long we want the dash to last
    private float dashCDTime;   //cooldown after once the dash is finished
    protected bool dashAvailable;
    public int playerId;
    float rot;


    #endregion

    #region Unit Stats
    [Header("Unit Stats:")]

    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] protected int speed;
    [SerializeField] private float dashingSpeed;
    [SerializeField] private float dashTimer;       //set the time of the dash
    [SerializeField] private float dashCDTimer;     //set the time for after the dash
    [SerializeField] protected LayerMask hitableLayer;
    [HideInInspector] public float speedMultiplier = 1;
    #endregion

    [HideInInspector] public Rigidbody rb;

    // // //

    virtual public void Init()
    {
        //speed = 10;
        isAlive = true;


        rb = GetComponent<Rigidbody>();
        // Debug.Log("basic init");
        rot = Camera.main.transform.rotation.eulerAngles.y;


        isHolding = false;
        dashAvailable = true;
        dashTime = dashTimer;

    }

    virtual public void UnitUpdate()
    {

        if (!dashAvailable)
        {
            dashCDTime -= Time.deltaTime;
            if (dashCDTime <= 0)
            {
                dashCDTime = dashCDTimer;
                dashAvailable = true;
            }
        }

    }

    virtual public void UnitFixedUpdate()
    {

    }

    virtual public void Death()
    {
        Debug.Log("basic isDead");
        isAlive = false;
    }

    virtual public void MovementAnimations()
    {
        //Debug.Log("basic animation");
    }


    virtual public void UseWeapon(Vector3 dir)
    {
        Debug.Log("yeet");

        Vector3 hitbox = transform.position + dir * 2;
        Collider[] collider = Physics.OverlapBox(hitbox,new Vector3(1,2,1), new Quaternion(),hitableLayer);
        foreach (Collider target in collider)
        {

            target.gameObject.GetComponent<BaseUnit>()?.TakeDamage(10);
        }
    }

    virtual public void UpdateMovement(Vector2 dir)
    {
        Vector3 _dir = new Vector3(dir.x, 0, dir.y);
        _dir = Camera.main.transform.TransformDirection(_dir);
        if(_dir != new Vector3())
        {
            rot = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg;
        }
        _dir.y = 0;
        Vector3 _dir2 = new Vector3(0, rb.velocity.y, 0);
        if (!isDashing)
        {
            transform.rotation = Quaternion.Euler(0, rot, 0);
            _dir = Vector3.ClampMagnitude(_dir * speed * speedMultiplier + _dir2, speed);
            rb.velocity = _dir;
        }
        else
            DashUpdate(dir);
    }

    public void ChangeSpeedMultiplier(float _speedMult)
    {
        speedMultiplier = _speedMult;

    }

    public void UseDash()
    {
        if (!isDashing)
        {
            isDashing = true;
        }
        //Debug.Log("Dash");
    }

    protected void DashUpdate(Vector2 dir)
    {
        dashTime -= Time.deltaTime;
        if (dashTime <= 0)
        {
            isDashing = false;
            dashTime = dashTimer;
            dashAvailable = false;
        }
        rb.velocity = dir * dashingSpeed;
    }

    public virtual void TakeDamage(float dmg)
    {
        if (isAlive && !isDashing)      //will only take damage if he is alive and is not invincible from dashing
        {
            health -= dmg;
            if (health <= 0)
            {
                isAlive = false;
                Death();
            }
            Debug.Log("basic takedamage " + dmg + " Remaining health : " + health + " Name : " + name);
        }
    }

}
