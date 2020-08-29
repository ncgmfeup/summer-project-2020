using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowAmmoNumber : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmoNumber(int numBullets, int clipSize, int numClips, bool autofire)
    {
        textMeshPro.text = numBullets.ToString();

    }
}
