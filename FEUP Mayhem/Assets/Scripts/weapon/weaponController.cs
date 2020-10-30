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

    public string WeaponType;

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

    [SerializeField]
    int shots_per_burst = 1;
    [SerializeField]
    int burst_max_angle = 20;

    public Transform weaponTransform;
    public GameObject bulletPrefab;

    //DAMAGE
    [SerializeField]
    float damage = 0.02f;

    [SerializeField]
    Vector2Int staticKnockback = new Vector2Int(1, 1);

    [SerializeField]
    Vector2 knockbackMultiplier = new Vector2(1, 1);


    void Awake()
    {
        remainingAmmo = clipSize;
        remainingClips = numClips;
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
            else if(remainingClips == 1 || remainingClips == 0)
            {
                return false;
            }
            else if(remainingClips == -1)
            {
                remainingAmmo = clipSize;
                return true;
            }
        }
        return true;
    }


    public void ShootBullet()
    {
        GameObject.Find("/Audio Objects/SFX/" + WeaponType).GetComponent<AudioSource>().Play();
        if (shots_per_burst == 1)
        {
            GameObject thisBullet;
            if (transform.rotation.eulerAngles.y >= 179.9f)
            {
                thisBullet = Instantiate(bulletPrefab, weaponTransform.position + new Vector3(-GunOffset.x, GunOffset.y, GunOffset.z), weaponTransform.rotation);
            }
            else
            {
                thisBullet = Instantiate(bulletPrefab, weaponTransform.position + new Vector3(GunOffset.x, GunOffset.y, GunOffset.z), weaponTransform.rotation);
            }
            bulletController bulletController = thisBullet.GetComponent<bulletController>();
            bulletController.SetDamageAndKnockback(damage, staticKnockback, knockbackMultiplier);
            bulletController.SetBulletForce(bulletForce);
            bulletController.SetPlayer(gameObject.transform.parent.gameObject);
            Destroy(thisBullet, bulletLifespan);
        }
        else
        {
            GameObject thisBullet;
            Quaternion gunRotation;

            float angle;

            for (int i = 0; i < shots_per_burst; i++){
                angle = -burst_max_angle + (burst_max_angle * 2f / ((float)(shots_per_burst - 1)) * ((float) i));
                gunRotation = weaponTransform.rotation;
                gunRotation = (Quaternion.Euler(gunRotation.eulerAngles.x, gunRotation.eulerAngles.y, gunRotation.eulerAngles.z + angle));
                if (transform.rotation.eulerAngles.y >= 179.9f)
                {
                    thisBullet = Instantiate(bulletPrefab, weaponTransform.position + new Vector3(-GunOffset.x, GunOffset.y, GunOffset.z), gunRotation);
                }
                else
                {
                    thisBullet = Instantiate(bulletPrefab, weaponTransform.position + new Vector3(GunOffset.x, GunOffset.y, GunOffset.z), gunRotation);
                }
                bulletController bulletController = thisBullet.GetComponent<bulletController>();
                bulletController.SetDamageAndKnockback(damage, staticKnockback, knockbackMultiplier);
                bulletController.SetBulletForce(bulletForce);
                bulletController.SetPlayer(gameObject.transform.parent.gameObject);
                Destroy(thisBullet, bulletLifespan);
            }
        }

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

    public int GetRemainingClips()
    {
        return remainingClips;
    }
}
