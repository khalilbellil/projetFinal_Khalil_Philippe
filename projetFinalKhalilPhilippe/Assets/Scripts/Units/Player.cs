using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseUnit
{
    Level lvl;
    CameraControler cam;
    Animator animator;
    float jumpForce = 30f;
    float jumpCD = 0.5f;
    bool isJumping = false;
    float timeOfNextValidJump;
    bool canJump { get { return Time.time >= timeOfNextValidJump && Physics.Raycast(gameObject.transform.position, new Vector3(0, -1, 0), 2); } }


    // BASIC FUNCTIONS //

    public void PlayerInit()
    {
        base.Init();

        //Camera Controller Init
        cam = GameObject.Find("Main Camera").GetComponent<CameraControler>();
        cam.Init();

        //Init Leveling System:
        lvl = new Level();
        lvl.InitLevel(1, 10, 0, 100);

        //Init UI:
        //UIManager.Instance.uiLinks = GetComponentInChildren<UILinks>();

        //Init Animator:
        InitAnimator();
    }

    public void PlayerUpdate()
    {
        base.UnitUpdate();
        cam.CameraUpdate();
    }

    public void PlayerFixedUpdate()
    {
        base.UnitFixedUpdate();

        Jump();
        UpdateMovement(InputManager.Instance.fixedInputPressed.dirPressed);
        if (InputManager.Instance.fixedInputPressed.leftMouseButtonPressed)
        {
            UseWeapon(transform.forward);
        }
    }


    // FUNCTIONS //

    public void Jump()
    {
        if (InputManager.Instance.fixedInputPressed.jumpPressed && canJump)
        {
            timeOfNextValidJump = Time.time + jumpCD;
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    void InitAnimator() //Set of all animator variables
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isJumping", isJumping);
        animator.SetFloat("forward", InputManager.Instance.fixedInputPressed.dirPressed.y);
    }

}
