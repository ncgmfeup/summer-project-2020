using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator anim;

    public void PlayCredits(){
        anim.SetTrigger("FadeOut");
        SceneManager.LoadScene("Credits");

    }
    
    public void ExitGame(){
        Debug.Log("Exiting");
        Application.Quit();
    }
}
