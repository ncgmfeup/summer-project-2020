using UnityEngine;

public class PickupGun : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.layer == LayerMask.NameToLayer("Player"))
        {
            if (obj.CompareTag("Player")){
                if (obj.GetComponent<ShootGun>().getGunScript() == null) {
                    transform.SetParent(collision.transform);
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.Euler(Vector3.zero);

                    obj.GetComponent<ShootGun>().setGunScript(gameObject.GetComponent<weaponController>());
                    transform.parent.gameObject.GetComponent<ShootGun>().UpdateUsedGun(true);
                    Destroy(gameObject.transform.GetChild(0).gameObject);
                    Destroy(gameObject.GetComponent<EdgeCollider2D>());
                    Destroy(gameObject.GetComponent<Rigidbody2D>());
                    Destroy(this);
                }
            }
        }
    }
}
