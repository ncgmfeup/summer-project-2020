using UnityEngine;

[RequireComponent(typeof(PlayerSpecs))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    public GameObject Dynamite;
    private PlayerSpecs specs;
    private Rigidbody2D rb;

    private bool canJump = true;

    private bool canUseDynamite = true;

    [SerializeField]
    private float jumpForce = 7.5f, moveSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        specs = GetComponent<PlayerSpecs>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Jump
        if (Input.GetButtonDown(specs.JumpButtonName()) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
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

        rb.velocity = new Vector2(horMove * moveSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;

        // Detect collisions with the floor
        if(col.tag == "Floor")
        {
            // If the character touched the floor, it can jump again
            canJump = true;
        }
    }

    public void SetDynamiteTrue(){
        canUseDynamite = true;
    }
}
