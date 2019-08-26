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

    public GameObject target;
    bool targetSetted = false;


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

        //Init Animator:
        InitAnimator();

        target = null;
    }

    public void PlayerUpdate()
    {
        base.UnitUpdate();
        cam.CameraUpdate();

        UpdateTarget();
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

    // Interaction Functions //

    public GameObject GetTargetFromRayCastForward()
    {
        GameObject retour = null;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.yellow);
            retour = hit.collider.gameObject;
        }

        return retour;
    }

    void UpdateTarget()
    {
        if (InputManager.Instance.inputPressed.interactPressed)
        {
            target = GetTargetFromRayCastForward(); //Get the target in front of us
            if (target != null && !targetSetted)
            {
                UIManager.Instance.uiLinks.targetUI.text += target.GetComponent<BaseUnit>().unitName;
                targetSetted = true;
                if (target.CompareTag("PNJ"))
                {
                    UIManager.Instance.uiLinks.pressKeyUI.SetActive(false);
                }
            }
            else
            {
                UIManager.Instance.uiLinks.targetUI.text = "LOCKED TARGET: ";
                target = null;
                targetSetted = false;
            }
        }
    }

}
