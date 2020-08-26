using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    //Gun related
    [SerializeField]
    weaponController defaultGunScript;
    public weaponController currentGunScript;
    //---

    //Input related
    [SerializeField]
    string inputString = "space";
    //---

    // Start is called before the first frame update
    void Start()
    {
        UpdateUsedGun(currentGunScript != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGunScript != null)
        {
            currentGunScript.AddGunCd(-Time.deltaTime);
            bool autoFire = currentGunScript.GetAutoFire();
            if (((!autoFire && Input.GetKeyDown(inputString)) || (autoFire && Input.GetKey(inputString))) && currentGunScript.GetGunCd() <= 0f)
            {
                bool result;
                currentGunScript.ShootBullet();
                result = currentGunScript.ResetCd();
                if (!result)
                {
                    defaultGunScript.SetCurrentCd(currentGunScript.GetMaxCd());
                    UpdateUsedGun(false);
                }
            }
        }
        else
        {
            defaultGunScript.AddGunCd(-Time.deltaTime);
            bool autoFire = defaultGunScript.GetAutoFire();
            if (((!autoFire && Input.GetKeyDown(inputString)) || (autoFire && Input.GetKey(inputString))) && defaultGunScript.GetGunCd() <= 0f)
            {
                defaultGunScript.ShootBullet();
                defaultGunScript.ResetCd();
            }
        }
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
            if(currentGunScript != null)
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
}
