using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerSpecs))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Double))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public GameObject Dynamite;
    private PlayerSpecs specs;
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
    private double multiplier = 0.2;
    private new Collider2D collider;

    private AudioSource sound;

    private string soundPrefix = "/Audio Objects/SFX/";


    private bool canJump = true, doubleJump = true, onPlatform = false;
    private bool isWalking = false, isJumping = false;

    private bool canUseDynamite = true;
    private bool moveEnabled = true;


    [SerializeField]
    private float jumpForce = 7.5f, moveSpeed = 100, doubleJumpMultiplier = 0.75f;

    /*
    [SerializeField]
    float smoothTime = 0.05f;
    [SerializeField]
    float smoothTimeHighSpeed = 0.025f;
    */

    Vector2 vel = Vector2.zero;
    float n = 1;


    Vector2 bottomLeft;
    Vector2 topRight;

    BoxCollider2D playerCol;


    // Start is called before the first frame update
    void Start()
    {
        playerCol = gameObject.GetComponent<BoxCollider2D>();

        specs = GetComponent<PlayerSpecs>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        multiplier = 0.01;
    }

    private void Update()
    {
        bottomLeft = new Vector2(transform.position.x, transform.position.y) + (playerCol.offset - playerCol.size / 2f) * transform.localScale;
        topRight = new Vector2(transform.position.x, transform.position.y) + (playerCol.offset + new Vector2(playerCol.size.x / 2f, 0f) - new Vector2(0f, playerCol.size.y / 2f)) * transform.localScale;

        string[] layers = { "Platform", "Platform_Expect_2", "Ground"};
        Collider2D[] col = Physics2D.OverlapAreaAll(bottomLeft, topRight, LayerMask.GetMask(layers));
        //Debug.Log(col.Length);
        canJump = false;
        onPlatform = false;
        for (int i = 0; i < col.Length; i++)
        {
            CheckHitbox(col[i]);
        }

        if (moveEnabled)
        {
            // Jump
            if (Input.GetButtonDown(specs.JumpButtonName()))
            {
                sound = GameObject.Find(soundPrefix + "Jump").GetComponent<AudioSource>();
                if (canJump)
                {
                    isJumping = true;
                    rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                    canJump = false;
                    sound.Play();
                }
                else if (doubleJump)
                {
                    isJumping = true;
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.AddForce(Vector3.up * jumpForce * doubleJumpMultiplier, ForceMode2D.Impulse);
                    doubleJump = false;
                    sound.Play();

                }

            }
            else if (Input.GetButtonDown(specs.JumpDownName()))
            {
                if (onPlatform)
                {
                    JumpDownPlatform(col);
                    //collider.isTrigger = true;
                }
            }

            // Horizontal Movement
            float horMove = Input.GetAxis(specs.HorizontalAxisName());

            if (Input.GetButton(specs.DynamiteButtonName()) && canUseDynamite)
            {
                Instantiate(Dynamite, transform.position, transform.rotation);
                canUseDynamite = false;
                Invoke("SetDynamiteTrue", 1);
            }

            // Flip character to the left or right
            if (horMove < 0)
            {
                isWalking = true;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (horMove > 0)
            {
                isWalking = true;
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                isWalking = false;
            }

            if (rb.velocity.x < moveSpeed)
            {
                rb.velocity = Vector2.MoveTowards(rb.velocity, new Vector2(horMove * moveSpeed, rb.velocity.y), moveSpeed * Time.deltaTime * 60f);

            // rb.velocity = Vector2.MoveTowards(rb.velocity, new Vector2(horMove * moveSpeed, rb.velocity.y), moveSpeed * Time.deltaTime * 60f);
            //rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(horMove * moveSpeed, rb.velocity.y), ref vel, smoothTime);
            }
            else
            {
                rb.velocity = Vector2.MoveTowards(rb.velocity, new Vector2(horMove * moveSpeed, rb.velocity.y), moveSpeed * Time.deltaTime * 30f);

                //rb.velocity = Vector2.MoveTowards(rb.velocity, new Vector2(horMove * moveSpeed * Time.deltaTime, rb.velocity.y), moveSpeed * Time.deltaTime);
                //  Debug.Log("A");
                //rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(horMove * moveSpeed, rb.velocity.y), ref vel, smoothTimeHighSpeed);
            }
            if (rb.velocity.y < -0.0001f)
            {
                rb.gravityScale = 2.5f;
            }
            else
            {
                rb.gravityScale = 1.5f;
            }

            animator.SetBool("walking", isWalking);
            animator.SetBool("jumping", isJumping);
        }
    }

    private void CheckHitbox(Collider2D col)
    {
        
        // Detect collisions with the floor 
        if (col.CompareTag("Floor"))
        {
            // If the character touched the floor, it can jump again
            ResetJumpValues();
        }
        else if (col.CompareTag("Platform"))
        {
            if (rb.velocity.y <= 0f)
            {
                onPlatform = true;

                // If the character touched a platform, it can jump again
                ResetJumpValues();
            }
        }
    }


    private void JumpDownPlatform(Collider2D[] col)
    {
        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].CompareTag("Platform"))
            {
                onPlatform = false;
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), col[i], true);
            }
        }
    }
 
    public double GetMultiplier()
    {
        return multiplier;
    }

    public void stun()
    {
        moveEnabled = false;
        StartCoroutine(WaitThreeSeconds());
    }

    public void IncreaseMultiplier(double inc)
    {
        multiplier += inc;
        GameObject.Find(soundPrefix + "Collision").GetComponent<AudioSource>().Play();
    }

    private void ResetJumpValues()
    {
        
        //GameObject.Find(soundPrefix + "Land").GetComponent<AudioSource>().Play();
        isJumping = false;
        canJump = true;
        doubleJump = true;
    }

    public void SetDynamiteTrue()
    {
        canUseDynamite = true;
    }

    private IEnumerator WaitThreeSeconds()
    {
        yield return new WaitForSeconds(3);
        moveEnabled = true;
    }

    public void ExitPlatform(Collider2D col)
    {
        
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), col, false);
        onPlatform = true;
    }
}
