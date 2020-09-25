using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerSpecs))]
public class MechPerk : MonoBehaviour
{
    private PlayerSpecs specs;
    private ShootGun enemyShootGun;

    private float cooldown = 5f;
    private float perkEffectTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        specs = GetComponent<PlayerSpecs>();

        // Get Enemy GameObject
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(player.name != name)
            {
                enemyShootGun = player.GetComponent<ShootGun>();
                break;
            }
        }

        StartCoroutine(Perk());
    }

    private IEnumerator Perk()
    {
        while (true)
        {
            if (Input.GetButtonDown(specs.PerkButtonName()))
            {
                //Change enemy weapon direction
                enemyShootGun.RotateWeapon();

                yield return new WaitForSeconds(perkEffectTime);

                //Reset enemy weapon direction
                enemyShootGun.RotateWeapon(false);

                yield return new WaitForSeconds(cooldown);
            }
            else
            {
                yield return null;
            }
        }
    }
}
