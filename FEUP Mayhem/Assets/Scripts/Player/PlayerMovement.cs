using UnityEngine;

[RequireComponent(typeof(PlayerSpecs))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    private PlayerSpecs specs;
    private Rigidbody2D rb;

    private bool canJump = true;

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
        if (Input.GetButtonDown(specs.JumpButtonName()) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }

        float horMove = Input.GetAxis(specs.HorizontalAxisName());
        if(horMove < 0)
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

        if(col.tag == "Floor")
        {
            canJump = true;
        }
    }
}
