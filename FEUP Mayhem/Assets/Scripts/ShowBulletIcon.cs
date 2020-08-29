using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBulletIcon : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;


    [SerializeField]
    Image img;
    Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateImage(int numBullets, int clipSize, int numClips, bool autofire)
    {
        float percentage = (float) numBullets / (float) clipSize;

        Debug.Log((int) (percentage * 5));
        Debug.Log(img.sprite);

        int index = (int)(percentage * 6);
        if (index > 5) index = 5;

        if (sprite != sprites[index])
        {
            img.sprite = sprites[index];
        }

    }
}
