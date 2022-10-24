using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Actions : MonoBehaviour
{
    [SerializeField] float JumpSpeed = .01f;
    [SerializeField] float PunchMoveAmt = 2f;
    [SerializeField] float HeavyReactAmt = 2f;
    [SerializeField] AudioClip PunchWoosh;
    [SerializeField] AudioClip KickWoosh;
    public GameObject Player1;
    private AnimatorStateInfo Player1Layer0;
    private Animator animator;
    private bool HeavyMoving = false;
    private bool HeavyReact = false;
    private AudioSource MyPlayer;
    public static bool Hits = false;
    public static bool FlyingJumpP1 = false;


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
            if (Player1Move.FacingRight)
            {
                Player1.transform.Translate(PunchMoveAmt * Time.deltaTime, 0, 0);
            }
            else
            {
                Player1.transform.Translate(-PunchMoveAmt * Time.deltaTime, 0, 0);
            }
        }

        //Heavy React Slide
        if (HeavyReact)
        {
            if (Player1Move.FacingRight)
            {
                Player1.transform.Translate(-HeavyReactAmt * Time.deltaTime, 0, 0);
            }
            else
            {
                Player1.transform.Translate(HeavyReactAmt * Time.deltaTime, 0, 0);
            }
        }

        //Listen to the Animator
        Player1Layer0 = animator.GetCurrentAnimatorStateInfo(0);

/*        //Standing Attacks
        if (Player1Layer0.IsTag("Motion"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("Light Punch");
                Hits = false;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                animator.SetTrigger("Heavy Punch");
                Hits = false;
            }
            if (Input.GetButtonDown("Fire3"))
            {
                animator.SetTrigger("Light Kick");
                Hits = false;
            }
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Heavy Kick");
                Hits = false;
            }
            if (Input.GetButtonDown("Block"))
            {
                animator.SetTrigger("Block On");
            }
        }

        //If no longer blocking
        if (Player1Layer0.IsTag("Block"))
        {
            if (Input.GetButtonUp("Block"))
            {
                animator.SetTrigger("Block Off");
            }
        }

        //Crouch Attacks
        if (Player1Layer0.IsTag("Crouching"))
        {
            if (Input.GetButtonDown("Fire3"))
            {
                animator.SetTrigger("Light Kick");
            }
        }

        //Arial Attacks
        if (Player1Layer0.IsTag("Jumping"))
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
        Player1.transform.Translate(0, JumpSpeed, 0);
    }

    public void FlipUp()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
        FlyingJumpP1 = true;
    }

    public void FlipBack()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
        FlyingJumpP1 = true;
    }

    public void IdleSpeed()
    {
        FlyingJumpP1 = false;
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
        if (Player1Layer0.IsTag("Motion"))
        {
            animator.SetTrigger("Heavy Punch");
            Hits = false;
        }
    }

    public void OnLightPunch()
    {
        if (Player1Layer0.IsTag("Motion"))
        {
            animator.SetTrigger("Light Punch");
            Hits = false;
        }
    }

    public void OnHeavyKick()
    {
        if (Player1Layer0.IsTag("Motion"))
        {
            animator.SetTrigger("Heavy Kick");
            Hits = false;
        }
    }

    public void OnLightKick()
    {
        if (Player1Layer0.IsTag("Motion"))
        {
            animator.SetTrigger("Light Kick");
            Hits = false;
        }
    }
}
