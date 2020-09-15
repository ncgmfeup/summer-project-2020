using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeOut = null;

    private void FadeOut()
    {
        fadeOut.SetActive(true);
    }
}
