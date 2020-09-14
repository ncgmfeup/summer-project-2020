using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBar : MonoBehaviour
{
    private Vector2 pos;
    public GameObject sprite;

    private GameObject icon;

    void Start(){
        pos = new Vector2(20,40);
        icon = Instantiate(sprite);
    }

    public void StartReloading(){
        pos = new Vector2(transform.position.x, transform.position.y + 1);
        icon.GetComponent<Transform>().position = pos;
        icon.SetActive(true); 
    }

    public void EndReload(){
        icon.SetActive(false);
    }

}
