using UnityEngine;

[RequireComponent(typeof(PlayerSpecs))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerSpecs specs;
    private Rigidbody2D rb;
    private new Collider2D collider;

    private bool canJump = true, onPlatform = false;

    [SerializeField]
    private float jumpForce = 7.5f, moveSpeed = 100;

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
            } //TODO else double jump

        }else if (Input.GetButtonDown(specs.JumpDownName()))
        {
            if (onPlatform)
            {
                collider.isTrigger = true;
            }
        }

        // Horizontal Movement
        float horMove = Input.GetAxis(specs.HorizontalAxisName());
        
        // Flip character to the left or right
        if (horMove < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(horMove > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }

        rb.velocity = new Vector2(horMove * moveSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;

        // Detect collisions with the floor 
        if(col.CompareTag("Floor"))
        {
            // If the character touched the floor, it can jump again
            canJump = true;
        }
        else if (col.CompareTag("Platform"))
        {
            onPlatform = true;

            // If the character touched a platform, it can jump again
            canJump = true;
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
}
