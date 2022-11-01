using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2ActionsAI : MonoBehaviour
{
    [SerializeField] float JumpSpeed = .01f;
    [SerializeField] float PunchMoveAmt = 2f;
    [SerializeField] float HeavyReactAmt = 2f;
    [SerializeField] AudioClip PunchWoosh;
    [SerializeField] AudioClip KickWoosh;
    public GameObject Player2;
    private bool HeavyReact = false;
    private AnimatorStateInfo Player2Layer0;
    private Animator animator;
    private bool HeavyMoving = false;
    private AudioSource MyPlayer;
    public static bool HitsAI = false;
    public static bool FlyingJumpAI = false;

    private int attackNumber = 1;
    private bool canAttack = true;
    public float attackRate = 1f;
    public static bool dazed = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Heavy Move Attack
        if (HeavyMoving)
        {
            if (Player2MoveAI.FacingRightAI)
            {
                Player2.transform.Translate(PunchMoveAmt * Time.deltaTime, 0, 0);
            }
            else
            {
                Player2.transform.Translate(-PunchMoveAmt * Time.deltaTime, 0, 0);
            }
        }

        //Heavy React Slide
        if (HeavyReact)
        {
            if (Player2MoveAI.FacingRightAI)
            {
                Player2.transform.Translate(-HeavyReactAmt * Time.deltaTime, 0, 0);
            }
            else
            {
                Player2.transform.Translate(HeavyReactAmt * Time.deltaTime, 0, 0);
            }
        }

        //Listen to the Animator
        Player2Layer0 = animator.GetCurrentAnimatorStateInfo(0);

        //Standing Attacks
        if (Player2Layer0.IsTag("Motion"))
        {
            if (canAttack)
            {
                if (attackNumber == 1)
                {
                    animator.SetTrigger("Light Punch");
                    HitsAI = false;
                }
                if (attackNumber == 2)
                {
                    animator.SetTrigger("Heavy Punch");
                    HitsAI = false;
                }
                if (attackNumber == 3)
                {
                    animator.SetTrigger("Light Kick");
                    HitsAI = false;
                }
                if (attackNumber == 4)
                {
                    animator.SetTrigger("Heavy Kick");
                    HitsAI = false;
                }
/*                if (Input.GetButtonDown("Block P2"))
                {
                    animator.SetTrigger("Block On");
                }*/
                canAttack = false;
            }
        }

        //If no longer blocking
        if (Player2Layer0.IsTag("Block"))
        {
            if (Input.GetButtonUp("Block P2"))
            {
                animator.SetTrigger("Block Off");
            }
        }

        //Crouch Attacks
        if (Player2Layer0.IsTag("Crouching"))
        {
            animator.SetTrigger("Light Kick");
            HitsAI = false;
            animator.SetBool("Crouch", false);
        }

        //Arial Attacks
        if (Player2Layer0.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Heavy Kick");
            }
        }
    }

    public void PunchWooshSound()
    {
        MyPlayer.clip = PunchWoosh;
        MyPlayer.Play();
    }
    public void KickWooshSound()
    {
        MyPlayer.clip = KickWoosh;
        MyPlayer.Play();
    }

    public void JumpUp()
    {
        Player2.transform.Translate(0, JumpSpeed, 0);
    }

    public void FlipUp()
    {
        Player2.transform.Translate(0, JumpSpeed, 0);
        FlyingJumpAI = true;
    }

    public void FlipBack()
    {
        Player2.transform.Translate(0, JumpSpeed, 0);
        FlyingJumpAI = true;
    }

    public void IdleSpeed()
    {
        FlyingJumpAI = false;
    }

    public void HeavyMove()
    {
        StartCoroutine(PunchSlide());
    }

    public void HeavyReaction()
    {
        StartCoroutine(HeavySlide());
        attackNumber = 3;
    }

    public void RandomAttack()
    {
        attackNumber = Random.Range(1, 5);
        StartCoroutine(SetAttacking());
    }

    IEnumerator PunchSlide()
    {
        HeavyMoving = true;
        yield return new WaitForSeconds(0.1f);
        HeavyMoving = false;
    }

    IEnumerator HeavySlide()
    {
        HeavyReact = true;
        dazed = true;
        yield return new WaitForSeconds(.3f);
        HeavyReact = false;
        yield return new WaitForSeconds(3f);
        dazed = false;
    }

    IEnumerator SetAttacking()
    {
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }
}
