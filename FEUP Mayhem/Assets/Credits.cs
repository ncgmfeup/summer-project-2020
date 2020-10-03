using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeOut = null;

    private void FadeOut()
    {
        fadeOut.SetActive(true);
    }

    void ExitCredits(){
        SceneManager.LoadScene("MainMenu");
    }
}
