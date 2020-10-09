using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public int number_of_lives = 3;
    [SerializeField]
    public Text livesText;
    [SerializeField]
    public GameObject victory;
    [SerializeField]
    public GameObject shadow;
    [SerializeField]
    public GameObject buttons;

    // Start is called before the first frame update
    void Start()
    {
        //Set lives GUI
        string nLivesString = number_of_lives.ToString();
        livesText.text = nLivesString;
    
    }

    public void death()
    {
        number_of_lives--;
        string nLivesString = number_of_lives.ToString();
        livesText.text = nLivesString;

        if(number_of_lives == 0)
            lost();        
    }

    void lost()
    {
        victory.SetActive(true);
        shadow.SetActive(true);
        buttons.SetActive(true);
    }
}
