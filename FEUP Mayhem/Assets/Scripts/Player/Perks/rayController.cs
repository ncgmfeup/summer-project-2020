using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayController : MonoBehaviour
{
    private Rigidbody2D rayRB;

    GameObject player;
    
    PlayerMovement enemy;

    // Start is called before the first frame update
    void Start()
    {
        rayRB = this.GetComponent<Rigidbody2D>();
        rayRB.gravityScale = 0;
        rayRB.AddForce(transform.right * 500f);
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
                    enemy = collision.gameObject.GetComponent<PlayerMovement>();
                    enemy.stun();
                    Destroy(this.gameObject);
                    return;
                }
            }
        }
    }
    
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
