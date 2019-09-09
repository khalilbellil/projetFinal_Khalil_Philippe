using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseUnit
{
    public Quest trackedQuest;

    Level lvl;
    CameraControler cam;
    Animator animator;
    float jumpForce = 40f;
    float jumpCD = 0.5f;
    bool isJumping = false;
    float timeOfNextValidJump = 0;
    bool canJump { get { return Time.time >= timeOfNextValidJump && Physics.Raycast(gameObject.transform.position + new Vector3(0, .1f, 0), new Vector3(0, -1, 0), 2, LayerMask.GetMask("Ground")); } }

    public GameObject target;
    bool targetSetted = false;

    public bool pressKeyAvailable;


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
        pressKeyAvailable = false;

        
    }

    public void PlayerUpdate()
    {
        if (isAlive)
        {
            base.UnitUpdate();
            cam.CameraUpdate();
            UpdateDialogue();
        }
        else
        {
            //GAMEOVER UI
            UIManager.Instance.OpenGameOverUI();
        }
    }

    public void PlayerFixedUpdate()
    {
        if (isAlive)
        {
            base.UnitFixedUpdate();
            Jump();
            UpdateMovement(InputManager.Instance.fixedInputPressed.dirPressed);
            UpdateAnims();
            if (InputManager.Instance.fixedInputPressed.leftMouseButtonPressed)
            {
                UseWeapon(transform.forward);
                animator.SetTrigger("AttackTrigger");
            }
        }
    }


    // FUNCTIONS //

    public void NotifyPlayer(string txt, int time)
    {
        StartCoroutine(UIManager.Instance.LaunchNotifyUI(txt, time));
    }

    public void Jump()
    {
        if (InputManager.Instance.fixedInputPressed.jumpPressed && canJump)
        {
            timeOfNextValidJump = Time.time + jumpCD;
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            animator.SetTrigger("jumpTrigger");
        }
    }

    void InitAnimator() //Set of all animator variables
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("RunFloat", currentSpeed);

    }

    void UpdateAnims()
    {
        animator.SetFloat("RunFloat", currentSpeed);
        animator.SetFloat("PlayerHp", health);
    }

    // Interaction Functions //

    public void UpdateDialogue()
    {

        //Dialogue System:
        if (InputManager.Instance.inputPressed.interactPressed)
        {
            if (target != null)
            {
                if (pressKeyAvailable) //Launch dialogue
                {
                    DialogueManager.Instance.SetNewDialogue(target.GetComponent<PNJ>().dialogue, target.GetComponent<PNJ>().pnjName);
                    DialogueManager.Instance.LaunchDialogue();
                    UIManager.Instance.ClosePressKeyUI();
                    pressKeyAvailable = false;
                }
                else if (UIManager.Instance.dialogueUIActive)
                {
                    if (DialogueManager.Instance.thereIsTextToShow)
                    {
                        DialogueManager.Instance.LaunchDialogue();
                    }
                    else //Dialogue was finished -> now display the quest proposition
                    {
                        if (target.GetComponent<PNJ>().pnjToTalk && target.GetComponent<PNJ>().questTracker.attachedQuest.nbEnnemiesToKill == 0 && !target.GetComponent<PNJ>().questTracker.attachedQuest.talkToDone)
                        {
                            //DialogueManager.Instance.LaunchQuestDialogue(target.GetComponent<PNJ>().questTracker.attachedQuest.questName, target.GetComponent<PNJ>().questTracker.description);
                            UIManager.Instance.CloseDialogueUI();
                            UIManager.Instance.SetDialogueUI(target.GetComponent<PNJ>().questTracker.attachedQuest.questName, target.GetComponent<PNJ>().questTracker.description);
                            UIManager.Instance.OpenDialogueUI();

                            target.GetComponent<PNJ>().questTracker.attachedQuest.talkToDone = true;
                            target.GetComponent<PNJ>().questTracker.attachedQuest.pnjNamesToTalk.Remove(target.GetComponent<PNJ>().pnjName); //Notify quest that talkTo is done
                        }
                        else if (!target.GetComponent<PNJ>().pnjToTalk && target.GetComponent<PNJ>().thereIsQuestToPropose && !DialogueManager.Instance.questWasProposed && !target.GetComponent<PNJ>().questAccepted)
                        {
                            DialogueManager.Instance.LaunchQuestDialogue(target.GetComponent<PNJ>().myQuest.questName, target.GetComponent<PNJ>().myQuest.description);
                            DialogueManager.Instance.questWasProposed = true;
                        }
                        else //Stop Dialogue
                        {
                            DialogueManager.Instance.FinishDialogue();
                        }
                    }
                }
            }

        }
    }

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
                UIManager.Instance.uiLinks.pressKeyUI.SetActive(false);
            }
            else
            {
                UIManager.Instance.uiLinks.targetUI.text = "LOCKED TARGET: ";
                target = null;
                targetSetted = false;
            }
        }
    }

    public override void Death()
    {
        base.Death();
        animator.SetTrigger("DeathTrigger");
        transform.position = new Vector3(transform.position.x, -1, transform.position.z);
    }
}
