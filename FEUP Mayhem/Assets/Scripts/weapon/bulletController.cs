using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    private Rigidbody2D bulletRB;
    public float bulletForce;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = this.GetComponent<Rigidbody2D>();
        bulletRB.gravityScale = 0;
        bulletRB.AddForce(transform.right * bulletForce);
        Destroy(this.gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        Destroy(this.gameObject);
    }
}
