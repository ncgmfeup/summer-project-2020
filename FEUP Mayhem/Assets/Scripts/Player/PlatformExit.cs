using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformExit : MonoBehaviour
{
    PlayerMovement playerMovScript;
    // Start is called before the first frame update
    void Start()
    {
        playerMovScript = transform.parent.gameObject.GetComponent<PlayerMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("A");
        playerMovScript.ExitPlatform(collision.GetComponent<Collider2D>());
    }
}
