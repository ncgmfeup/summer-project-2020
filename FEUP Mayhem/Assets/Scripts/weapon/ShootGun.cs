using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    //Gun related
    [SerializeField]
    weaponController defaultGunScript;
    public weaponController currentGunScript;


    //Last frame's gun controller (failsafe for picking up guns)
    weaponController gunScriptLastFrame;
    //---

    //Input related
    [SerializeField]
    string inputString = "space";
    //---


    float currentGunCd = 0;

    // Start is called before the first frame update
    void Start()
    {
        gunScriptLastFrame = getCorrectGunScript();
        UpdateUsedGun(currentGunScript != null);
    }

    // Update is called once per frame
    void Update()
    {
        weaponController gunScript = getCorrectGunScript();

        currentGunCd -= Time.deltaTime;
        bool autoFire = gunScript.GetAutoFire();



        if (((!autoFire && Input.GetKeyDown(inputString)) || (autoFire && Input.GetKey(inputString))) && currentGunCd <= 0f)
        {
            bool result;
            gunScript.ShootBullet();
            currentGunCd = gunScript.GetMaxCd();

            if (gunScript)
            {

                if (gunScript != defaultGunScript)
                {
                    result = gunScript.SubtractAmmo();
                    if (!result)
                    {
                        Destroy(gunScript.gameObject);
                    }
                }
            }
        }


        if (gunScript != gunScriptLastFrame)
        {
            if (gunScript == defaultGunScript)
            {
                UpdateUsedGun(false);
            }
            else
            {
                UpdateUsedGun(true);
            }
        }
        gunScriptLastFrame = getCorrectGunScript();

    }

    public void UpdateUsedGun(bool hasSpecialGun)
    {
        if (hasSpecialGun)
        {
            defaultGunScript.gameObject.SetActive(false);
            currentGunScript.gameObject.SetActive(true);
        }
        else
        {
            defaultGunScript.gameObject.SetActive(true);
            if (currentGunScript != null)
            {
                Destroy(currentGunScript.gameObject);
            }
        }
    }

    public void setGunScript(weaponController gun)
    {
        currentGunScript = gun;
    }

    public weaponController getGunScript()
    {
        return currentGunScript;
    }

    public weaponController getCorrectGunScript()
    {
        return currentGunScript == null ? defaultGunScript : currentGunScript;
    }

}