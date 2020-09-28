using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(WaitToMove(collision.transform));
    }

    private IEnumerator WaitToMove(Transform collisionTransform)
    {
        yield return new WaitForSeconds(2f);
        collisionTransform.position = respawnPoint.position;
    }
}
