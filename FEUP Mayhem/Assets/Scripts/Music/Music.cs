using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    private static Music instance = null;

    [SerializeField] private string audioSourceScene = "";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            if (audioSourceScene.Equals(instance.GetAudioSourceScene()))
            {
                Destroy(gameObject);
                return;
            }

            Destroy(instance.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
            PlayerPrefs.SetInt("Muted", 1);
        else
            PlayerPrefs.SetInt("Muted", 0);
    }

    public string GetAudioSourceScene() => audioSourceScene;
}
