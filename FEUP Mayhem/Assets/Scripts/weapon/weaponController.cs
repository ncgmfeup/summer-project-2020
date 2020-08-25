using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController : MonoBehaviour
{
    //New code: 
    // - Add the possibility of auto fire using an if and Input.GetKey if with autofire and Input.GetKeyDown if without (done)
    // - Add time between shots (done)
    // - Add reload time (done)
    // - Make the input string serializable in editor (done)
    // - Implement number of shots per ammo clip and total shots, which can be infinite (done)
    // - Implement shot lifetime (done)

    // - Fazer as armas dropar
    // - Alocar balas quando a arma é apanhada (Probably not doing this)


    //Propriedades das balas
    [SerializeField]
    float bulletForce = 500f;
    [SerializeField]
    float bulletLifespan = 1.5f;

    


    //Propriedades da arma
    [SerializeField]
    float maxGunCd = 0.5f;
    [SerializeField]
    float maxReloadCd = 2f;
    float gunCd = 0f;


    [SerializeField]
    bool autoFire = false;

    [SerializeField]
    int clipSize = 5;
    int remainingAmmo;

    [SerializeField]
    int numClips = 10;
    int remainingClips;

    //--

    public Transform weaponTransform;
    public GameObject bulletPrefab;

    void Start()
    {
        remainingAmmo = clipSize;
        remainingClips = numClips;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public bool GetAutoFire()
    {
        return autoFire;
    }

    public void AddGunCd(float time)
    {
        gunCd += time;
    }
    
    public void ResetCd()
    {
        if(remainingAmmo > 1)
        {
            remainingAmmo--;
            gunCd = maxGunCd;
        }
        else
        {
            if (remainingClips > 1) {
                remainingAmmo = clipSize;
                remainingClips--;
                gunCd = maxReloadCd;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public float GetGunCd()
    {
        return gunCd;
    }

    public void ShootBullet()
    {
        GameObject thisBullet;
        thisBullet = Instantiate(bulletPrefab, weaponTransform.position, weaponTransform.rotation);
        thisBullet.GetComponent<bulletController>().SetBulletForce(bulletForce);
        Destroy(thisBullet, bulletLifespan);
    }
}
