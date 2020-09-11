using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapons : MonoBehaviour
{
    float match_time = 0;
    float next_time = 5f;
    float interval = 25f;
    [SerializeField]
    Vector2 leftWeaponPosition = new Vector2(-4, 6);
    [SerializeField]
    Vector2 rightWeaponPosition = new Vector3(4, 6);

    [SerializeField]
    GameObject[] guns;

    GameObject leftGun;
    GameObject rightGun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        match_time += Time.deltaTime;
        if(match_time >= next_time)
        {
            SpawnWeaponForEach();
            next_time += interval;
        }
    }

    public void SpawnWeaponForEach()
    {
        if(leftGun == null)
        {

            leftGun = Instantiate(guns[Random.Range(0, guns.Length)]);
            leftGun.transform.position = leftWeaponPosition;
        }
        else if (leftGun.GetComponent<PickupGun>() == null)
        {
            leftGun = Instantiate(guns[Random.Range(0, guns.Length)]);
            leftGun.transform.position = leftWeaponPosition;
        }

        if(rightGun == null)
        {
            rightGun = Instantiate(guns[Random.Range(0, guns.Length)]);
            rightGun.transform.position = rightWeaponPosition;
        }
        else if (rightGun.GetComponent<PickupGun>() == null)
        {
            rightGun = Instantiate(guns[Random.Range(0, guns.Length)]);
            rightGun.transform.position = rightWeaponPosition;
        }
    }

}
