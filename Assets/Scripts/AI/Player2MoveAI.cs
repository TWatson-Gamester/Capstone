using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2MoveAI : MonoBehaviour
{
    [SerializeField] float walkSpeed = .001f;
    [SerializeField] float jumpSpeed = .05f;
    [SerializeField] GameObject Player2;
    [SerializeField] GameObject Opponent;
    [SerializeField] AudioClip LightPunch;
    [SerializeField] AudioClip HeavyPunch;
    [SerializeField] AudioClip LightKick;
    [SerializeField] AudioClip HeavyKick;
    [SerializeField] Rigidbody RB;
    [SerializeField] Collider BoxCollider;
    [SerializeField] Collider CapsuleCollider;
    private Vector3 OppPosition;
    private Animator animator;
    private bool IsJumping = false;
    private AnimatorStateInfo Player2Layer0;
    private bool CanWalkLeft = true;
    private bool CanWalkRight = true;
    public static bool FacingLeftAI = false;
    public static bool FacingRightAI = true;
    public static bool walkLeftAI = true;
    public static bool walkRightAI = true;
    private AudioSource audioSource;
    public GameObject Restrict;
    private float MoveSpeed;

    private float OppDistance;
    public float AttackDistance = 1.5f;
    private bool MoveAI = true;
    private bool isBlocking = false;
    public static bool AttackState = false;
    private int defend = 0;

    void Start()
    {
        Opponent = GameObject.Find("Player 1");
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
        StartCoroutine(RightIsTrue());
        MoveSpeed = walkSpeed;
    }

    void Update()
    {
        OppDistance = Vector3.Distance(Opponent.transform.position, Player2.transform.position);
        if (Player1Actions.FlyingJumpP1)
        {
            walkSpeed = jumpSpeed;
        }
        else walkSpeed = MoveSpeed;

        //Check if knocked out
        if (SaveScript.Player2Health <= 0)
        {
            animator.SetTrigger("Knocked Out");
            Player2.GetComponent<Player2ActionsAI>().enabled = false;
            this.GetComponent<Player2MoveAI>().enabled = false;
        }
        if (SaveScript.Player1Health <= 0)
        {
            animator.SetTrigger("Victory");
            Player2.GetComponent<Player2ActionsAI>().enabled = false;
            this.GetComponent<Player2MoveAI>().enabled = false;
        }

        //Listen to the Animator
        Player2Layer0 = animator.GetCurrentAnimatorStateInfo(0);

        //Cannot Exit Screen
        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);
        if (ScreenBounds.x > Screen.width - 200)
            CanWalkRight = false;
        else if (ScreenBounds.x < 200)
            CanWalkLeft = false;
        else
        {
            CanWalkLeft = true;
            CanWalkRight = true;
        }

        //Get Opponent's Position
        OppPosition = Opponent.transform.position;

        if (!Player2ActionsAI.dazed && Time.timeScale == 1)
        {
            //Flip around to face opponent
            if (OppPosition.x < Player2.transform.position.x)
            {
                StartCoroutine(LeftIsTrue());

                if (Player2Layer0.IsTag("Motion"))
                {
                    animator.SetBool("CanAttack", false);
                    if (OppDistance > AttackDistance)
                    {
                        if (MoveAI)
                        {
                            if (CanWalkRight)
                            {
                                if (walkRightAI)
                                {
                                    animator.SetBool("Forward", true);
                                    animator.SetBool("Backward", false);
                                    AttackState = false;
                                    transform.Translate(walkSpeed, 0, 0);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (CanWalkLeft)
                        {
                            if (MoveAI)
                            {
                                MoveAI = false;
                                animator.SetBool("Forward", false);
                                animator.SetBool("Backward", false);
                                animator.SetBool("CanAttack", true);
                                StartCoroutine(ForwardFalse());
                            }
                        }
                    }
                }
            }
            else if (OppPosition.x > Player2.transform.position.x)
            {
                StartCoroutine(RightIsTrue());

                if (Player2Layer0.IsTag("Motion"))
                {
                    animator.SetBool("CanAttack", false);
                    if (OppDistance > AttackDistance)
                    {
                        if (MoveAI)
                        {
                            if (CanWalkLeft)
                            {
                                if (walkLeftAI)
                                {
                                    animator.SetBool("Forward", false);
                                    animator.SetBool("Backward", true);
                                    AttackState = false;
                                    transform.Translate(-walkSpeed, 0, 0);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (CanWalkLeft)
                        {
                            if (MoveAI)
                            {
                                MoveAI = false;
                                animator.SetBool("Forward", false);
                                animator.SetBool("Backward", false);
                                animator.SetBool("CanAttack", true);
                                StartCoroutine(ForwardFalse());
                            }
                        }
                    }
                }
            }
        }

        //Jumping and Crouching
        if (Input.GetAxis("Vertical Key 2") > 0)
        {
            if (!IsJumping)
            {
                IsJumping = true;
                animator.SetTrigger("Jump");
                StartCoroutine(JumpPause());
            }
        }
        if (defend == 2)
        {
            if (!isBlocking)
            {
                isBlocking = true;
                animator.SetTrigger("Block On");
                StartCoroutine(EndBlock());
            }
        }
        if(defend == 3)
        {
            animator.SetBool("Crouch", true);
            defend = 0;
        }
        if(defend == 4)
        {
            animator.SetTrigger("Jump");
            defend = 0;
        }

        //Reset the restrict
        if (!Restrict.gameObject.activeInHierarchy)
        {
            walkLeftAI = true;
            walkRightAI = true;
        }

        if (Player2Layer0.IsTag("Block"))
        {
            RB.isKinematic = true;
            BoxCollider.enabled = false;
            CapsuleCollider.enabled = false;
        }
        else if (Player2Layer0.IsTag("Motion"))
        {
            BoxCollider.enabled = true;
            CapsuleCollider.enabled = true;
            RB.isKinematic = false;
        }

        if (Player2Layer0.IsTag("Crouching"))
        {
            BoxCollider.enabled = false;
        }
        if (Player2Layer0.IsTag("Sweep"))
        {
            BoxCollider.enabled = false;
        }
        if (Player2Layer0.IsTag("React"))
        {
            BoxCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FistLight"))
        {
            animator.SetTrigger("Light React");
            audioSource.clip = LightPunch;
            audioSource.Play();
            defend = Random.Range(0, 5);
        }
        if (other.gameObject.CompareTag("FistHeavy"))
        {
            animator.SetTrigger("Heavy React");
            audioSource.clip = HeavyPunch;
            audioSource.Play();
        }
        if (other.gameObject.CompareTag("KickLight"))
        {
            animator.SetTrigger("Light React");
            audioSource.clip = LightKick;
            audioSource.Play();
            defend = Random.Range(0, 5);
        }
        if (other.gameObject.CompareTag("KickHeavy"))
        {
            animator.SetTrigger("Heavy React");
            audioSource.clip = HeavyKick;
            audioSource.Play();
        }
    }

    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
    }

    IEnumerator LeftIsTrue()
    {
        if (FacingLeftAI)
        {
            FacingLeftAI = false;
            FacingRightAI = true;
            yield return new WaitForSeconds(0.15f);
            //Player2.transform.Rotate(0, -180, 0);
            transform.rotation = Quaternion.AngleAxis(-180, Vector3.up);
            animator.SetLayerWeight(1, 0);
        }
    }

    IEnumerator RightIsTrue()
    {
        if (FacingRightAI)
        {
            FacingLeftAI = true;
            FacingRightAI = false;
            yield return new WaitForSeconds(0.15f);
            //Player2.transform.Rotate(0, 180, 0);
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            animator.SetLayerWeight(1, 1);
        }
    }

    IEnumerator ForwardFalse()
    {
        yield return new WaitForSeconds(.6f);
        MoveAI = true;
    }

    IEnumerator EndBlock()
    {
        yield return new WaitForSeconds(1.5f);
        isBlocking = false;
        animator.SetTrigger("Block Off");
        defend = 0;
    }
}
