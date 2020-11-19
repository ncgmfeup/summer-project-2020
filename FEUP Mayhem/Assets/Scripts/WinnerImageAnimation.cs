using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerImageAnimation : MonoBehaviour
{
    private List<Sprite> idleSprites = null;

    [SerializeField]
    private float animationTime = .5f;

    [SerializeField]
    private Image image; 

    public void SetIdleSprites(List<Sprite> sprites)
    {
        idleSprites = sprites;
        StopAllCoroutines();
        gameObject.SetActive(true);
        if(sprites.Count == 1)
        {
            image.sprite = sprites[0];
        }
        else
        {
            StartCoroutine(Animate());
        }
    }

    private IEnumerator Animate()
    {
        uint curSprite = 0;

        while (true)
        {
            image.sprite = idleSprites[(int)(curSprite % idleSprites.Count)];
            curSprite++;
            yield return new WaitForSeconds(animationTime);
        }
    }
}
