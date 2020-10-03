using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayCredits(){
        SceneManager.LoadScene("Credits");

    }
    
    public void ExitGame(){
        Debug.Log("Exiting");
        Application.Quit();
    }
}
