using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            if(collision.gameObject.GetComponent<ShootGun>().getGunScript() == null){
                transform.SetParent(collision.transform);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.Euler(Vector3.zero);
                collision.gameObject.GetComponent<ShootGun>().setGunScript(gameObject.GetComponent<weaponController>());
                Destroy(gameObject.transform.GetChild(0).gameObject);
                Destroy(gameObject.GetComponent<EdgeCollider2D>());
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                Destroy(this);
            }
        }
    }
}
