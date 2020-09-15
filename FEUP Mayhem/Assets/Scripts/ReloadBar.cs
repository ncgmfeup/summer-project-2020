using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBar : MonoBehaviour
{
    [SerializeField]
    private Vector2 extra_position = new Vector2(0, 0.5f);

    //SpriteRenderer spr;
    BoxCollider2D col;


    private Vector2 pos;
    public GameObject sprite;

    private GameObject icon;
    private Transform icon_transform;

    void Start(){
        col = gameObject.GetComponent<BoxCollider2D>();
        icon = Instantiate(sprite);
        icon_transform = icon.transform;
        icon_transform.SetParent(transform);
    }

    public void StartReloading(){

        pos = extra_position + new Vector2(transform.position.x, transform.position.y + (col.size.y / 2f * transform.localScale.y));
        icon_transform.position = pos;
        icon.SetActive(true); 
    }

    public void EndReload(){
        icon.SetActive(false);
    }

}
