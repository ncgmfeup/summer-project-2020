using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    float bulletForce;

    GameObject player;

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
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                if (collision.gameObject != player)
                {
                    Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                    double multiplier = collision.GetComponent<PlayerMovement>().GetMultiplier();
                    if (multiplier < 3)
                        collision.GetComponent<PlayerMovement>().IncreaseMultiplier(0.02);
                    if(transform.rotation.eulerAngles.y < -90f || transform.rotation.eulerAngles.y > 90f) {
                        rb.AddForce(new Vector2(-(float)(multiplier * 15f), 2f), ForceMode2D.Impulse);
                    }
                    else
                    {
                        rb.AddForce(new Vector2((float)(multiplier * 10f), 2f), ForceMode2D.Impulse);
                    }
                    //Debug.Log("moved (" + ((float)(multiplier * 10f)).ToString() + ", 1)");
                    Destroy(this.gameObject);
                }
            }
        }
        else if(collision.CompareTag("Wall_Perk"))
        {
            collision.gameObject.GetComponent<Wall_Perk_Health>().SubtractHealth(0.02f);
            Destroy(this.gameObject);
        }
        /*
        else
        {
            Destroy(this.gameObject);
        }
        */
    }

    public void SetBulletForce(float bulletForce)
    {
        this.bulletForce = bulletForce;
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
