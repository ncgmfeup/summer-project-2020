using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerSpecs))]
public class InfElecPerk : MonoBehaviour
{
    private PlayerSpecs specs;
    public Transform rayTransform;
    public GameObject rayPrefab;

    private float cooldown = 5f;
    private float rayLifespan = 1f;

    // Start is called before the first frame update
    void Start()
    {
        specs = GetComponent<PlayerSpecs>();

        StartCoroutine(Perk());
    }

    private IEnumerator Perk()
    {
        while (true)
        {
            if (Input.GetButtonDown(specs.PerkButtonName()))
            {
                GameObject thisRay;
                Quaternion rayRotation;
                Vector3 Offset = Vector2.zero;

                float angle;

                for (int i = 0; i < 5; i++){
                    angle = -20 + (20 * 2f / (4f) * ((float) i));
                    rayRotation = rayTransform.rotation;
                    rayRotation = (Quaternion.Euler(rayRotation.eulerAngles.x, rayRotation.eulerAngles.y, rayRotation.eulerAngles.z + angle));
                    if (transform.rotation.eulerAngles.y >= 179.9f)
                    {
                        thisRay = Instantiate(rayPrefab, rayTransform.position + new Vector3(-Offset.x, Offset.y, Offset.z), rayRotation);
                    }
                    else
                    {
                        thisRay = Instantiate(rayPrefab, rayTransform.position + new Vector3(Offset.x, Offset.y, Offset.z), rayRotation);
                    }
                    rayController rayController = thisRay.GetComponent<rayController>();
                    rayController.SetPlayer(gameObject);
                    Destroy(thisRay, rayLifespan);
                }

                yield return new WaitForSeconds(cooldown);
            }
            else
            {
                yield return null;
            }
        }
    }
}
