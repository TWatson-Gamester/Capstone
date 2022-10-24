using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Move : MonoBehaviour
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
    public static bool FacingLeftP2 = false;
    public static bool FacingRightP2 = true;
    public static bool walkLeft = true;
    public static bool walkRight = true;
    private AudioSource audioSource;
    public GameObject Restrict;
    private float MoveSpeed;

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
        if (Player1Actions.FlyingJumpP1)
        {
            walkSpeed = jumpSpeed;
        }
        else walkSpeed = MoveSpeed;

        //Check if knocked out
        if (SaveScript.Player2Health <= 0)
        {
            animator.SetTrigger("Knocked Out");
            Player2.GetComponent<Player2Actions>().enabled = false;
            this.GetComponent<Player2Move>().enabled = false;
        }
        if (SaveScript.Player1Health <= 0)
        {
            animator.SetTrigger("Victory");
            Player2.GetComponent<Player2Actions>().enabled = false;
            this.GetComponent<Player2Move>().enabled = false;
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

        //Flip around to face opponent
        if(OppPosition.x > Player2.transform.position.x)
        {
            StartCoroutine(LeftIsTrue());
        }
        else if(OppPosition.x < Player2.transform.position.x)
        {
            StartCoroutine(RightIsTrue());
        }

        //Horizontal Movement
        if (Player2Layer0.IsTag("Motion"))
        {
            Time.timeScale = 1;
            if (Input.GetAxis("Horizontal Key 2") > 0)
            {
                if(CanWalkRight)
                {
                    if (walkRight)
                    {
                        animator.SetBool("Forward", true);
                        transform.Translate(walkSpeed, 0, 0);
                    }
                }
            }
            if (Input.GetAxis("Horizontal Key 2") < 0)
            {
                if(CanWalkLeft)
                {
                    if (walkLeft)
                    {
                        animator.SetBool("Backward", true);
                        transform.Translate(-walkSpeed, 0, 0);
                    }
                }
            }
        }
        if (Input.GetAxis("Horizontal Key 2") == 0)
        {
            animator.SetBool("Forward", false);
            animator.SetBool("Backward", false);
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
        if (Input.GetAxis("Vertical Key 2") < 0)
        {
            animator.SetBool("Crouch", true);
        }
        if (Input.GetAxis("Vertical Key 2") == 0)
        {
            animator.SetBool("Crouch", false);
        }

        //Reset the restrict
        if (!Restrict.gameObject.activeInHierarchy)
        {
            walkLeft = true;
            walkRight = true;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FistLight"))
        {
            animator.SetTrigger("Light React");
            audioSource.clip = LightPunch;
            audioSource.Play();
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
        if (FacingLeftP2)
        {
            FacingLeftP2 = false;
            FacingRightP2 = true;
            yield return new WaitForSeconds(0.15f);
            Player2.transform.Rotate(0, -180, 0);
            animator.SetLayerWeight(1, 0);
        }
    }

    IEnumerator RightIsTrue()
    {
        if (FacingRightP2)
        {
            FacingLeftP2 = true;
            FacingRightP2 = false;
            yield return new WaitForSeconds(0.15f);
            Player2.transform.Rotate(0, 180, 0);
            animator.SetLayerWeight(1, 1);
        }
    }
}
