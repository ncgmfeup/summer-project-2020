using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    float bulletForce;


    //DAMAGE
    float damage = 0.02f;

    Vector2Int staticKnockback = new Vector2Int(1, 1);
    Vector2 knockbackMultiplier = new Vector2(1, 1);


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
                        collision.GetComponent<PlayerMovement>().IncreaseMultiplier(damage);
                    int direc = 1;
                    if(transform.rotation.eulerAngles.y < -90f || transform.rotation.eulerAngles.y > 90f) {
                        direc = -1;
                        //rb.AddForce(new Vector2(-(float)(multiplier * 15f) * knockbackMultiplier.x, 2f * knockbackMultiplier.y), ForceMode2D.Impulse);
                    }/*
                    else
                    {*/
                    if (staticKnockback.x == 0 && staticKnockback.y == 0)
                        {
                            rb.AddForce(new Vector2(direc * (10f * knockbackMultiplier.x), 20f * knockbackMultiplier.y), ForceMode2D.Impulse);
                        }
                        else if (staticKnockback.x == 0)
                        {
                            rb.AddForce(new Vector2(direc * (10f * knockbackMultiplier.x), 20f * knockbackMultiplier.y * (float)multiplier), ForceMode2D.Impulse);
                        }
                        else if (staticKnockback.y == 0)
                        {
                            rb.AddForce(new Vector2(direc * ((float)(Math.Pow(multiplier, 0.5) * 80f) * knockbackMultiplier.x), 2f * knockbackMultiplier.y), ForceMode2D.Impulse);
                        }
                        else {
                            rb.AddForce(new Vector2(direc * ((float)(multiplier * 80f) * knockbackMultiplier.x), 2f * knockbackMultiplier.y * (float)multiplier), ForceMode2D.Impulse);
                        }
                    //}
                    //Debug.Log("moved (" + ((float)(multiplier * 10f)).ToString() + ", 1)");
                    Destroy(this.gameObject);
                }
            }
        }
        else if(collision.CompareTag("Wall_Perk"))
        {
            collision.gameObject.GetComponent<Wall_Perk_Health>().SubtractHealth(damage);
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

    public void SetDamageAndKnockback(float damage, Vector2Int staticKnockback, Vector2 knockbackMultiplier)
    {
        this.damage = damage;
        this.staticKnockback = staticKnockback;
        this.knockbackMultiplier = knockbackMultiplier;
    }
}
