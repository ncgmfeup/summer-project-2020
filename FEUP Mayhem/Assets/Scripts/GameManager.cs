using System;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player1 = null, player2 = null;

    [SerializeField]
    private List<RuntimeAnimatorController> charactersAnimators = null;

    [SerializeField]
    private List<string> charactersPerks = null;

    [SerializeField]
    private int eletroIndex = 1;

    [SerializeField]
    private List<string> idleSpritesFolder = null;

    private string spritesP1, spritesP2;

    [SerializeField]
    private WinnerImageAnimation winnerImageAnimation = null;

    private bool changeColor = false;

    // Start is called before the first frame update
    void Start()
    {
        // PLAYER 1

        Animator animator1 = player1.GetComponent<Animator>();
        int index1 = PlayerPrefs.GetInt("CharacterP1");

        animator1.runtimeAnimatorController = charactersAnimators[index1];

        if (index1 == eletroIndex) player1.AddComponent<Glitch>();

        player1.AddComponent(Type.GetType(charactersPerks[index1]));

        spritesP1 = idleSpritesFolder[index1];

        // PLAYER 2

        Animator animator2 = player2.GetComponent<Animator>();
        int index2 = PlayerPrefs.GetInt("CharacterP2");

        animator2.runtimeAnimatorController = charactersAnimators[index2];

        if (index2 == eletroIndex) player2.AddComponent<Glitch>();

        player2.AddComponent(Type.GetType(charactersPerks[index2]));

        spritesP2 = idleSpritesFolder[index2];

        if(index1 == index2)
        {
            SpriteRenderer sp2 = player2.GetComponent<SpriteRenderer>();
            sp2.color = new Color(.5f, .5f, .5f);

            changeColor = true;
        }
    }

    public void SetWinningCharacterAnimation(string winningCharacter)
    {
        string folder;
        bool rotateImage;
        bool color = changeColor;

        if(winningCharacter == "Player 1")
        {
            color = false;
            rotateImage = false;
            folder = "IdleSprites/" + spritesP1;
        }
        else
        {
            rotateImage = true;
            folder = "IdleSprites/" + spritesP2;
        }

        List<Sprite> sprites = new List<Sprite>();
        foreach(UnityEngine.Object obj in Resources.LoadAll(folder, typeof(Sprite))) {
            sprites.Add((Sprite)obj);
        }

        winnerImageAnimation.SetIdleSprites(sprites, rotateImage, color);
    }
}
