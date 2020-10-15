using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioPerk : MonoBehaviour
{
    private PlayerSpecs specs;
    private GameObject poisonCloudPrefab;
    public Collider2D playerCollider;
    private Transform playerTransform;
    private float cooldown = 20f;

    // Start is called before the first frame update
    void Start()
    {
        specs = GetComponent<PlayerSpecs>();                                    //
        playerTransform = GetComponent<Transform>();                            //
        playerCollider = GetComponent<Collider2D>();                            //  gets stuff
        poisonCloudPrefab = Resources.Load<GameObject>("Prefabs/cloud");        //
        StartCoroutine(Perk());
    }
    
    private IEnumerator Perk()
    {
        while (true)
        {
            if (Input.GetButtonDown(specs.PerkButtonName()))
            {
                GameObject instantiatedCloud = Instantiate(poisonCloudPrefab,playerTransform.position, Quaternion.Euler(0,0,0));    //instantiates cloud
                PoisonCloud spawnedCloud = instantiatedCloud.GetComponent<PoisonCloud>();                                           //gets reference to the cloud's PoisonCloud object
                spawnedCloud.spawnerName = this.gameObject.name;                                                                    //changes the spawnerName name in the cloud's script

                yield return new WaitForSeconds(cooldown);          //waits for perk cooldown
            }
            else
            {
                yield return null;
            }
        }
    }

}
