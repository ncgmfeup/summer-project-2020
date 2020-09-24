using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField]
    private CharacterManager characterManager = null;
    
    [SerializeField] int player;

    private GameObject[] characterList;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        //Fill array
        characterList = new GameObject[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
            characterList[i] = transform.GetChild(i).gameObject;

        //hide
        foreach (GameObject go in characterList)
            go.SetActive(false);

        //Show one
        index = PlayerPrefs.GetInt("CharacterP" + player);
        if(characterList[index])
            characterList[index].SetActive(true);
    }

    public void ToggleLeft()
    {
        characterList[index].SetActive(false);

        index--;
        if(index < 0)
            index = characterList.Length - 1;

        characterList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        characterList[index].SetActive(false);

        index++;
        if(index == characterList.Length)
            index = 0;

        characterList[index].SetActive(true);
    }

    public void Ready()
    {
        characterManager.SetReady(player);
        PlayerPrefs.SetInt("CharacterP" + player, index);
    }
}
