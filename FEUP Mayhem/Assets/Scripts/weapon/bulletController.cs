using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    float bulletForce;
    bool shot;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = this.GetComponent<Rigidbody2D>();
        bulletRB.gravityScale = 0;
        bulletRB.AddForce(transform.right * bulletForce);
        shot = false;
        
        //Destroy(this.gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!shot)
                shot = true;
            else
            {
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                double multiplier = collision.GetComponent<PlayerMovement>().GetMultiplier();
                if (multiplier < 1)
                    collision.GetComponent<PlayerMovement>().IncreaseMultiplier(0.03);
                rb.AddForce(new Vector2((float)(multiplier * 10000000f), 2f), ForceMode2D.Impulse);
                Debug.Log("moved (" + ((float)(multiplier * 10000000f)).ToString() + ", 1)");
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetBulletForce(float bulletForce)
    {
        this.bulletForce = bulletForce;
    }
}
