using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAutoFire : MonoBehaviour
{

    public void SetTrueAutofire(int numBullets, int clipSize, int numClips, bool autofire)
    {
        gameObject.SetActive(autofire);
    }

    public void SetFalseAutofire(int numBullets, int clipSize, int numClips, bool autofire)
    {
        gameObject.SetActive(!autofire);
    }
}
