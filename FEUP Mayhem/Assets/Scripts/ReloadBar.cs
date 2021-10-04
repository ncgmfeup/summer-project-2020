using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBar : MonoBehaviour
{
    [SerializeField]
    private Vector2 extra_position = new Vector2(0, 0.5f);

    [SerializeField]
    private Vector2 load_bar_offset = new Vector2(-0.5f, 0);
    //SpriteRenderer spr;
    BoxCollider2D col;


    private Vector2 pos;
    public GameObject sprite;
    public GameObject reloadBarFill;

    private GameObject sprite_icon;
    private GameObject reload_bar_icon;


    float maxDuration = 0;
    float duration = 0;

    void Start(){
        col = gameObject.GetComponent<BoxCollider2D>();
        Transform rotateImmune = gameObject.transform.Find("RotateImmune");
        sprite_icon = Instantiate(sprite);
        reload_bar_icon = Instantiate(reloadBarFill);
        sprite_icon.transform.SetParent(rotateImmune);
        reload_bar_icon.transform.SetParent(rotateImmune);
    }


    public void StartReloading(float duration)
    {
        pos = extra_position + new Vector2(transform.position.x, transform.position.y + (col.size.y / 2f * transform.localScale.y));
        sprite_icon.transform.position = pos;
        reload_bar_icon.transform.position = pos;
        sprite_icon.SetActive(true);
        reload_bar_icon.SetActive(true);
        this.duration = duration;
        this.maxDuration = duration;
        StartCoroutine(LoadBar());
    }

    public void EndReload(){
        duration = 0;
        sprite_icon.SetActive(false);
        reload_bar_icon.SetActive(false);
    }

    public IEnumerator LoadBar()
    {
        while(duration > 0)
        {
            duration -= Time.deltaTime;
            reload_bar_icon.transform.localPosition = new Vector3(-load_bar_offset.x * (duration / maxDuration), load_bar_offset.y + reload_bar_icon.transform.localPosition.y, reload_bar_icon.transform.localPosition.z);
            reload_bar_icon.transform.localScale = new Vector3(reload_bar_icon.transform.localScale.y * (1f - duration / maxDuration), reload_bar_icon.transform.localScale.y, reload_bar_icon.transform.localScale.z);
            yield return true;
        }
        sprite_icon.SetActive(false);
        reload_bar_icon.SetActive(false);
    }

}
