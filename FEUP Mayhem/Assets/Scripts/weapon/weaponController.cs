using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController : MonoBehaviour
{
    //TODO: 
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
    //float gunCd = 0f;


    [SerializeField]
    bool autoFire = false;

    [SerializeField]
    int clipSize = 5;
    int remainingAmmo;

    [SerializeField]
    int numClips = 10;
    int remainingClips;

    [SerializeField]
    Vector3 GunOffset = Vector2.zero;

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

    public bool SubtractAmmo()
    {
        if (remainingAmmo > 1)
        {
            remainingAmmo--;
        }
        else
        {
            if (remainingClips > 1)
            {
                remainingAmmo = clipSize;
                remainingClips--;
            }
            else
            {
                return false;
            }
        }
        return true;
    }


    public void ShootBullet()
    {
        GameObject thisBullet;
        thisBullet = Instantiate(bulletPrefab, weaponTransform.position + GunOffset, weaponTransform.rotation);
        thisBullet.GetComponent<bulletController>().SetBulletForce(bulletForce);
        Destroy(thisBullet, bulletLifespan);
    }

    public float GetMaxCd()
    {
        return maxGunCd;
    }

    public float GetMaxReloadCd()
    {
        return maxReloadCd;
    }

    public int GetAmmo()
    {
        return remainingAmmo;
    }

    public int GetClipSize()
    {
        return clipSize;
    }

    public int GetNumClips()
    {
        return numClips;
    }
}
