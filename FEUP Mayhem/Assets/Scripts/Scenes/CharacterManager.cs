using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    private bool readyP1 = false, readyP2 = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(readyP1 && readyP2){
            SceneManager.LoadScene("GameSection");

        }
    }   

    public void SetReady(int player)
    {
        if(player == 1 )
            readyP1 = true;
        else readyP2 = true;
    }
}
