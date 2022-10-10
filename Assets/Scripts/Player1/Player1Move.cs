using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    [SerializeField] float walkSpeed = .001f;
    [SerializeField] float jumpSpeed = .05f;
    [SerializeField] GameObject Player1;
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
    private AnimatorStateInfo Player1Layer0;
    private bool CanWalkLeft = true;
    private bool CanWalkRight = true;
    public static bool FacingLeft = false;
    public static bool FacingRight = true;
    public static bool walkLeftP1 = true;
    public static bool walkRightP1 = true;
    private AudioSource audioSource;
    public GameObject Restrict;
    private float MoveSpeed;
    private float Timer = 2;
    private float CrouchTimer = 2;

    void Start()
    {
        Opponent = GameObject.Find("Player 2");
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
        StartCoroutine(RightIsTrue());
        MoveSpeed = walkSpeed;
    }

    void Update()
    {
        if (Player1Actions.FlyingJumpP1)
        {
            walkSpeed = jumpSpeed;
        }
        else walkSpeed = MoveSpeed;
        //Check if knocked out
        if (SaveScript.Player1Health <= 0)
        {
            animator.SetTrigger("Knocked Out");
            Player1.GetComponent<Player1Actions>().enabled = false;
            this.GetComponent<Player1Move>().enabled = false;
        }
        if (SaveScript.Player2Health <= 0)
        {
            animator.SetTrigger("Victory");
            Player1.GetComponent<Player1Actions>().enabled = false;
            this.GetComponent<Player1Move>().enabled = false;
        }

        //Listen to the Animator
        Player1Layer0 = animator.GetCurrentAnimatorStateInfo(0);

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

        //Flip around to face opponent
        if(OppPosition.x > Player1.transform.position.x)
        {
            StartCoroutine(LeftIsTrue());
        }
        else if(OppPosition.x < Player1.transform.position.x)
        {
            StartCoroutine(RightIsTrue());
        }

        //Horizontal Movement
        if (Player1Layer0.IsTag("Motion"))
        {
            Time.timeScale = 1;
            if (Input.GetAxis("Horizontal") > 0)
            {
                if(CanWalkRight == true)
                {
                    if (walkRightP1)
                    {
                        animator.SetBool("Forward", true);
                        transform.Translate(walkSpeed, 0, 0);
                    }
                }
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                if(CanWalkLeft == true)
                {
                    if (walkLeftP1)
                    {
                        animator.SetBool("Backward", true);
                        transform.Translate(-walkSpeed, 0, 0);
                    }
                }
            }
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            animator.SetBool("Forward", false);
            animator.SetBool("Backward", false);
        }
        //Jumping and Crouching
        if (Input.GetAxis("Vertical") > 0)
        {
            if (!IsJumping)
            {
                IsJumping = true;
                animator.SetTrigger("Jump");
                StartCoroutine(JumpPause());
            }
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            if(CrouchTimer < Timer)
            {
                CrouchTimer += 1 * Time.deltaTime;
                animator.SetBool("Crouch", true);
            }
            else if(CrouchTimer > Timer)
            {
                animator.SetBool("Crouch", false);
                StartCoroutine(ResetCrouchTime());
            }
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            animator.SetBool("Crouch", false);
            StartCoroutine(ResetCrouchTime());
        }

        //Reset the restrict
        if (!Restrict.gameObject.activeInHierarchy)
        {
            walkLeftP1 = true;
            walkRightP1 = true;
        }

        if (Player1Layer0.IsTag("Block"))
        {
            RB.isKinematic = true;
            BoxCollider.enabled = false;
            CapsuleCollider.enabled = false;
        }
        else if(Player1Layer0.IsTag("Motion"))
        {
            BoxCollider.enabled = true;
            CapsuleCollider.enabled = true;
            RB.isKinematic = false;
        }

        if (Player1Layer0.IsTag("Crouching"))
        {
            BoxCollider.enabled = false;
        }
        if (Player1Layer0.IsTag("Sweep"))
        {
            BoxCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FistLight"))
        {
            animator.SetTrigger("Head React");
            audioSource.clip = LightPunch;
            audioSource.Play();
        }
        if (other.gameObject.CompareTag("FistHeavy"))
        {
            animator.SetTrigger("Big React");
            audioSource.clip = HeavyPunch;
            audioSource.Play();
        }
        if (other.gameObject.CompareTag("KickLight"))
        {
            animator.SetTrigger("Head React");
            audioSource.clip = LightKick;
            audioSource.Play();
        }
        if (other.gameObject.CompareTag("KickHeavy"))
        {
            animator.SetTrigger("Big React");
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
        if (FacingLeft)
        {
            FacingLeft = false;
            FacingRight = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, -180, 0);
            animator.SetLayerWeight(1, 0);
        }
    }

    IEnumerator RightIsTrue()
    {
        if (FacingRight)
        {
            FacingLeft = true;
            FacingRight = false;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);
            animator.SetLayerWeight(1, 1);
        }
    }

    IEnumerator ResetCrouchTime()
    {
        yield return new WaitForSeconds(2);
        CrouchTimer = 0;
    }
}
