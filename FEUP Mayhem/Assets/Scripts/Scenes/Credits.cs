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

    public void ExitCredits()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
