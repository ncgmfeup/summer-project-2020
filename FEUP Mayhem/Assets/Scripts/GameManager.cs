﻿using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player1 = null, player2 = null;

    [SerializeField]
    private List<RuntimeAnimatorController> charactersAnimators = null;

    [SerializeField]
    private int eletroIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        Animator animator1 = player1.GetComponent<Animator>();
        int index1 = PlayerPrefs.GetInt("CharacterP1");

        animator1.runtimeAnimatorController = charactersAnimators[index1];

        if (index1 == eletroIndex) player1.AddComponent<Glitch>();

        Animator animator2 = player2.GetComponent<Animator>();
        int index2 = PlayerPrefs.GetInt("CharacterP2");

        animator2.runtimeAnimatorController = charactersAnimators[index2];

        if (index2 == eletroIndex) player2.AddComponent<Glitch>();
    }
}