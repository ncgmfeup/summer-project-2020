using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    public Transform playerTransform;
    public Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //StartCoroutine(WaitToMove());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(WaitToMove());
    }

    private IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(2f);
        playerTransform.position = respawnPoint.position;
    }
}
