using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseUnit
{
    Level lvl;
    CameraControler cam;
    float jumpForce;
    float jumpCD = 0.5f;
    float timeOfNextValidJump;
    bool canJump { get { return Time.time >= timeOfNextValidJump && Physics.Raycast(gameObject.transform.position, new Vector3(0, -1, 0), 1); } }


    // BASIC FUNCTIONS //

    public void PlayerInit()
    {
        base.Init();
        cam = GameObject.Find("Main Camera").GetComponent<CameraControler>();
        cam.Init();
        Debug.Log("yeet");
        jumpForce = 50;

        //Init Leveling System:
        lvl = new Level();
        lvl.InitLevel(1, 10, 0, 100);
    }

    public void PlayerUpdate()
    {
        base.UnitUpdate();
        cam.CameraUpdate();
    }

    public void PlayerFixedUpdate()
    {
        base.UnitFixedUpdate();
        if (InputManager.Instance.fixedInputPressed.jumpPressed && canJump)
        {
            Jump();
        }
        UpdateMovement(InputManager.Instance.fixedInputPressed.dirPressed);
    }


    // FUNCTIONS //

    public void Jump()
    {
        timeOfNextValidJump = Time.time + jumpCD;
        rb.AddForce(new Vector3(0, jumpForce, 0),ForceMode.Impulse);
    }
}
