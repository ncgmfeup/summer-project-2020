using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowAmmoNumber : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMeshPro;
    public void UpdateNumClips(int numBullets, int clipSize, int numClips, bool autofire)
    {
        if (numClips < 0)
            textMeshPro.text = "∞";
        else
            textMeshPro.text = numClips.ToString();
    }

    public void UpdateAmmoNumber(int numBullets, int clipSize, int numClips, bool autofire)
    {
        textMeshPro.text = numBullets.ToString();
    }
}
