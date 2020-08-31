using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    float bulletForce;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = this.GetComponent<Rigidbody2D>();
        bulletRB.gravityScale = 0;
        bulletRB.AddForce(transform.right * bulletForce);
        
        //Destroy(this.gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        /*if (collision.tag == "Player")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            float multiplier = collision.GetComponent<multiplier>();
            if (multiplier < 1)
                multiplier += 0.02;
            rb.AddForce(new Vector2(multiplier*10f,1f), ForceMode2D.Impulse);
            Debug.Log("collided with enemy");
        }*/
        Destroy(this.gameObject);
    }

    public void SetBulletForce(float bulletForce)
    {
        this.bulletForce = bulletForce;
    }
}
