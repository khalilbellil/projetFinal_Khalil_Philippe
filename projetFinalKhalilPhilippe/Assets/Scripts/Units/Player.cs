using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseUnit
{
    float jumpForce;
    float jumpCD = 0.5f;
    float timeOfNextValidJump;
    bool canJump { get { return Time.time >= timeOfNextValidJump && Physics.Raycast(gameObject.transform.position, new Vector3(0, -1, 0), 1); } }
    // Start is called before the first frame update
    public void Init()
    {
        base.Init();
        rb = gameObject.GetComponent<Rigidbody>();
        jumpForce = 50;
    }

    // Update is called once per frame
    public void PlayerUpdate(InputManager.InputPkg input)
    {
        Debug.Log(input);
        //UpdateMovement(input.dirPressed);
        base.UnitUpdate(input.dirPressed);
    }

    public void PlayerFixedUpdate(InputManager.InputPkg input)
    {
        if(input.jumpPressed && canJump)
        {
            Jump();
        }
        UpdateMovement(input.dirPressed);
        base.UnitFixedUpdate();
    }

    public void Jump()
    {
        timeOfNextValidJump = Time.time + jumpCD;
        rb.AddForce(new Vector3(0, jumpForce, 0),ForceMode.Impulse);
    }
}
