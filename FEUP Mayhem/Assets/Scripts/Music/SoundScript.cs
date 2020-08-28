using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    private Music music;
    public Button musicToggleButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindObjectOfType<Music>();
        UpdateIcon();    
        UpdateVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseMusic(){
        music.ToggleSound();
        UpdateIcon();
        UpdateVolume();
    }

    void UpdateVolume(){
        if(PlayerPrefs.GetInt("Muted", 0) == 0)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
    }

    void UpdateIcon(){
        if(PlayerPrefs.GetInt("Muted", 0) == 0)
            musicToggleButton.GetComponent<Image>().sprite = musicOnSprite;
        else
            musicToggleButton.GetComponent<Image>().sprite = musicOffSprite;
    }
}
