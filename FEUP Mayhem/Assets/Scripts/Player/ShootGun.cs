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
                currentGunScript.ShootBullet();
                currentGunScript.ResetCd();
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
