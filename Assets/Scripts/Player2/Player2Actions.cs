using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Actions : MonoBehaviour
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
    public static bool HitsP2 = false;
    public static bool FlyingJumpP2 = false;


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
            if (Player2Move.FacingRightP2)
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
            if (Player2Move.FacingRightP2)
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

/*        //Standing Attacks
        if (Player2Layer0.IsTag("Motion"))
        {
            if (Input.GetButtonDown("Fire1 P2"))
            {
                animator.SetTrigger("Light Punch");
                HitsP2 = false;
            }
            if (Input.GetButtonDown("Fire2 P2"))
            {
                animator.SetTrigger("Heavy Punch");
                HitsP2 = false;
            }
            if (Input.GetButtonDown("Fire3 P2"))
            {
                animator.SetTrigger("Light Kick");
                HitsP2 = false;
            }
            if (Input.GetButtonDown("Jump P2"))
            {
                animator.SetTrigger("Heavy Kick");
                HitsP2 = false;
            }
            if (Input.GetButtonDown("Block P2"))
            {
                animator.SetTrigger("Block On");
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
            if (Input.GetButtonDown("Fire3"))
            {
                animator.SetTrigger("Light Kick");
            }
        }

        //Arial Attacks
        if (Player2Layer0.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Heavy Kick");
            }
        }*/
    }

    public void RandomAttack()
    {
        //Designed not to do anything
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
        FlyingJumpP2 = true;
    }

    public void FlipBack()
    {
        Player2.transform.Translate(0, JumpSpeed, 0);
        FlyingJumpP2 = true;
    }

    public void IdleSpeed()
    {
        FlyingJumpP2 = false;
    }

    public void HeavyMove()
    {
        StartCoroutine(PunchSlide());
    }

    public void HeavyReaction()
    {
        StartCoroutine(HeavySlide());
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
        yield return new WaitForSeconds(.3f);
        HeavyReact = false;
    }

    public void OnHeavyPunch()
    {
        if (Player2Layer0.IsTag("Motion"))
        {
            animator.SetTrigger("Heavy Punch");
            HitsP2 = false;
        }
    }

    public void OnLightPunch()
    {
        if (Player2Layer0.IsTag("Motion"))
        {
            animator.SetTrigger("Light Punch");
            HitsP2 = false;
        }
    }

    public void OnHeavyKick()
    {
        if (Player2Layer0.IsTag("Motion"))
        {
            animator.SetTrigger("Heavy Kick");
            HitsP2 = false;
        }
    }

    public void OnLightKick()
    {
        if (Player2Layer0.IsTag("Motion"))
        {
            animator.SetTrigger("Light Kick");
            HitsP2 = false;
        }
    }
}
