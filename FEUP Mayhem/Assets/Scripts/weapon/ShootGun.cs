using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ShootingEvent : UnityEvent<int, int, int, bool>
{
}

public class ShootGun : MonoBehaviour
{

    public ShootingEvent onShootingEvent;

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
            if (gunScript.GetAmmo() == 1)
            {
                currentGunCd = gunScript.GetMaxReloadCd();
            }
            else
            {
                currentGunCd = gunScript.GetMaxCd();
            }

            if (gunScript)
            {
                result = gunScript.SubtractAmmo();
                if (!result)
                {
                    Destroy(gunScript.gameObject);
                }
                weaponController temp = getCorrectGunScript();
                if (temp == defaultGunScript)
                {
                    onShootingEvent.Invoke(temp.GetAmmo() % temp.GetClipSize(), temp.GetClipSize(), -1, autoFire);
                }
                else {
                    onShootingEvent.Invoke(temp.GetAmmo() % temp.GetClipSize(), temp.GetClipSize(), temp.GetNumClips(), autoFire);
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