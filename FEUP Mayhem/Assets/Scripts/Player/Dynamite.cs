using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float power = 2.0f;
    public float radius = 3.0f;
    public float upForce = 1.0f;
    public float explosionTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        int playerRotation = 1;
        if (player.transform.eulerAngles.y == 180)
            playerRotation = -1;

        GetComponent<Rigidbody2D>().AddForce(new Vector3(playerRotation, 1) * 5f, ForceMode2D.Impulse);
        Invoke("Detonate", explosionTime);
    }


    void Detonate(){
        Vector2 ExpositionPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(ExpositionPosition, radius);
        foreach(Collider2D hit in colliders){

            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb && hit.gameObject.tag == "Player"){
                //Debug.Log(hit.name);
                Vector2 diff = new Vector2(hit.gameObject.transform.position.x, hit.gameObject.transform.position.y) - ExpositionPosition;
                diff.Normalize();
                rb.AddForce(power*diff*radius, ForceMode2D.Impulse);
                //AddExplosionForce(rb, power, ExpositionPosition, radius, upForce);
            }
        }
       StartCoroutine(Die());
    }

    IEnumerator Die(){
        yield return new WaitForSeconds(0.2f);       
        Destroy(gameObject); 
    }


    public static void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * (wearoff <= 0f ? 0f : explosionForce) * wearoff;
        body.AddForce(baseForce);
    }
 
    public static void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        AddExplosionForce(body, explosionForce, explosionPosition, explosionRadius);
 
        float upliftWearoff = 1 - upliftModifier / explosionRadius;
        Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
        body.AddForce(upliftForce);
    }

}
