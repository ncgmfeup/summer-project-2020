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
        rayPrefab = (GameObject) Resources.Load("Prefabs/Ray_Perk");
        rayTransform = rayPrefab.transform;
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
                Vector3 Offset = new Vector3(2f, 0, 0);

                float angle;

                for (int i = 0; i < 5; i++){
                    angle = -20 + (20 * 2f / (4f) * ((float) i));
                    rayRotation = rayTransform.rotation;
                    if (transform.rotation.eulerAngles.y >= 179.9f)
                    {
                        rayRotation = (Quaternion.Euler(rayRotation.eulerAngles.x, rayRotation.eulerAngles.y + 180, rayRotation.eulerAngles.z + angle));
                        thisRay = Instantiate(rayPrefab, rayTransform.position + new Vector3(-Offset.x, Offset.y, Offset.z), rayRotation);
                    }
                    else
                    {
                        rayRotation = (Quaternion.Euler(rayRotation.eulerAngles.x, rayRotation.eulerAngles.y, rayRotation.eulerAngles.z + angle));
                        thisRay = Instantiate(rayPrefab, rayTransform.position + new Vector3(Offset.x, Offset.y, Offset.z), rayRotation);
                    }
                    rayController rayController = thisRay.GetComponent<rayController>();
                    thisRay.transform.position = gameObject.transform.position;
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
