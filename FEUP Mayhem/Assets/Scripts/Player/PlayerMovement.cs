using UnityEngine;

[RequireComponent(typeof(PlayerSpecs))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    public GameObject Dynamite;
    private PlayerSpecs specs;
    private Rigidbody2D rb;
    private new Collider2D collider;


    private bool canJump = true, doubleJump = true, onPlatform = false;

    private bool canUseDynamite = true;

    //private float multiplier = 0;

    [SerializeField]
    private float jumpForce = 7.5f, moveSpeed = 100, doubleJumpMultiplier = 0.75f;

    [SerializeField]
    float smoothTime = 0.05f;

    Vector2 vel = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        specs = GetComponent<PlayerSpecs>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Jump
        if (Input.GetButtonDown(specs.JumpButtonName()))
        {
            if (canJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                canJump = false;
            }
            else if (doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector3.up * jumpForce * doubleJumpMultiplier, ForceMode2D.Impulse);
                doubleJump = false;
            }

        }
        else if (Input.GetButtonDown(specs.JumpDownName()))
        {
            //rb.AddForce(new Vector2(250f, 300f));
            if (onPlatform)
            {
                collider.isTrigger = true;
            }
        }

        // Horizontal Movement
        float horMove = Input.GetAxis(specs.HorizontalAxisName());

        if (Input.GetKey("1") && canUseDynamite){
            Instantiate(Dynamite, transform.position, transform.rotation);
            canUseDynamite = false;
            Invoke("SetDynamiteTrue", 1);
        }
        
        // Flip character to the left or right
        if (horMove < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(horMove > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }

        rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(horMove * moveSpeed, rb.velocity.y), ref vel, smoothTime);
        if(rb.velocity.y < -0.0001f)
        {
            rb.gravityScale = 2.5f;
        }
        else
        {
            rb.gravityScale = 1.5f;
        }
    }

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
            onPlatform = true;

            // If the character touched a platform, it can jump again
            ResetJumpValues();
        }
    }

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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            onPlatform = false;
            collider.isTrigger = false;
        }
    }

    private void ResetJumpValues()
    {
        canJump = true;
        doubleJump = true;
    }

    public void SetDynamiteTrue(){
        canUseDynamite = true;
    }
}
