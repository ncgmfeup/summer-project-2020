using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ShootingEvent : UnityEvent<int, int, int, bool>
{
}

[System.Serializable]
public class EndReloadEvent : UnityEvent<int, int, int, bool>
{
}

[System.Serializable]
public class SwapWeaponEvent : UnityEvent<int, int, int, bool>
{
}

public class ShootGun : MonoBehaviour
{

    public ShootingEvent onShootingEvent;

    public EndReloadEvent onEndReload;

    public SwapWeaponEvent onSwapWeapon;

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
                    onShootingEvent.Invoke(temp.GetAmmo() % temp.GetClipSize(), temp.GetClipSize(), temp.GetRemainingClips(), autoFire);
                }
            }
        }
        if (gunScript != gunScriptLastFrame)
        {
            weaponController temp = getCorrectGunScript();
            if (gunScript == defaultGunScript)
            {
                UpdateUsedGun(false);
                onSwapWeapon.Invoke(temp.GetAmmo(), temp.GetClipSize(), -1, temp.GetAutoFire());
            }
            else
            {
                UpdateUsedGun(true);
                onSwapWeapon.Invoke(temp.GetAmmo(), temp.GetClipSize(), temp.GetRemainingClips(), temp.GetAutoFire());
            }
        }


        weaponController tmp = getCorrectGunScript();
        //Debug.Log(tmp.GetRemainingClips());

        //Debug.Log(currentGunCd > -Time.deltaTime && currentGunCd < 0f && tmp.GetAmmo() == 0);
        if (currentGunCd > -Time.deltaTime && currentGunCd < 0f && tmp.GetAmmo() == tmp.GetClipSize())
        {
            onEndReload.Invoke(tmp.GetAmmo(), tmp.GetClipSize(), tmp.GetRemainingClips(), tmp.GetAutoFire());
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