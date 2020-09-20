using System;
using UnityEngine;

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


    private bool canJump = true, doubleJump = true, onPlatform = false;
    private bool isWalking = false, isJumping = false;

    private bool canUseDynamite = true;


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

        /*
        //Thanks to Baste on the unity forums for this snippet of code!
        int myLayer = gameObject.layer;
        int layerMask = 0;
        for(int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(myLayer, i))
            {
                layerMask = layerMask | 1 << i;
            }
        }*/
        string[] layers = { "Platform", "Platform_Expect_2", "Ground"};
        Collider2D[] col = Physics2D.OverlapAreaAll(bottomLeft, topRight, LayerMask.GetMask(layers));
        //Debug.Log(col.Length);
        canJump = false;
        onPlatform = false;
        for (int i = 0; i < col.Length; i++)
        {
            //Debug.Log(col[i].gameObject.name);
            //Debug.Log(col[i].gameObject.layer);
            CheckHitbox(col[i]);
        }

        // Jump
        if (Input.GetButtonDown(specs.JumpButtonName()))
        {
            if (canJump)
            {
                isJumping = true;
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                canJump = false;
            }
            else if (doubleJump)
            {
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector3.up * jumpForce * doubleJumpMultiplier, ForceMode2D.Impulse);
                doubleJump = false;
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

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;

        // Detect collisions with the floor 
        if(col.CompareTag("Floor"))
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
    */
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            onPlatform = true;
            collider.isTrigger = false;
        }else if(collision.CompareTag("Floor"))
        {
            collider.isTrigger = false;
            ResetJumpValues();
        }
    }*/

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
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            onPlatform = false;
            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if(collision.gameObject.layer == LayerMask.NameToLayer("Platform_Except_1"))
                {
                    collision.gameObject.layer = LayerMask.NameToLayer("Platform");
                }
                else if(collision.gameObject.layer == LayerMask.NameToLayer("Platform_Except_Both"))
                {
                    collision.gameObject.layer = LayerMask.NameToLayer("Platform_Except_2");
                }
                else
                {
                    Debug.Log("A");
                }
            }
            else if (gameObject.layer == LayerMask.NameToLayer("Player2"))
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Platform_Except_2"))
                {
                    collision.gameObject.layer = LayerMask.NameToLayer("Platform");
                }
                else if (collision.gameObject.layer == LayerMask.NameToLayer("Platform_Except_Both"))
                {
                    collision.gameObject.layer = LayerMask.NameToLayer("Platform_Except_1");
                }
                else
                {
                    Debug.Log("B");
                }
            }
            //collider.isTrigger = false;

        }
    }*/

    public double GetMultiplier()
    {
        return multiplier;
    }

    public void IncreaseMultiplier(double inc)
    {
        multiplier += inc;
    }

    private void ResetJumpValues()
    {
        isJumping = false;
        canJump = true;
        doubleJump = true;
    }

    public void SetDynamiteTrue()
    {
        canUseDynamite = true;
    }



    public void ExitPlatform(Collider2D col)
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), col, false);
        onPlatform = true;
    }
}
