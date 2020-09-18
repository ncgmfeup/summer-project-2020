using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    public Transform playerTransform;
    public Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerTransform.position = respawnPoint.position;
    }
}
