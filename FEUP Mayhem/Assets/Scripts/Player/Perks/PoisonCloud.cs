﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    private PlayerMovement movementScript;              //reference to a PlayerMovement object
    [HideInInspector] public string spawnerName;        //holds the name of the object that spawned the cloud
    [SerializeField] private double multiplierIncrement = 0.5;      //change value later
    [SerializeField] private float perkDuration = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndPerk());      //Starts the coroutine that destroys the cloud when the effect is over

        //to ignore collisions with the player that spawned the cloud
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
            if(player.name == spawnerName) {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            }
            else{                                                               //
                movementScript = player.GetComponent<PlayerMovement>();         //  since there are only 2 players
            }                                                                   //
        }
    }

    // private void OnTriggerStay2D(Collider2D other){
    //     movementScript = other.gameObject.GetComponent<PlayerMovement>();           if the game has more than 2 players, use this instead of the else statement above
    // }

    private void OnTriggerEnter2D(Collider2D other){
        StartCoroutine(Poison());       //start the poison coroutine once the cloud touches a player
    }

    
    private void OnTriggerExit2D(Collider2D other){
        StopCoroutine(Poison());        //end the poison coroutine once the player is no longer touching a cloud
    }
    
    private IEnumerator Poison(){
        while(true){
            yield return new WaitForSeconds(1.0f);                            //every 1 sec
            movementScript.IncreaseMultiplier(multiplierIncrement);           //increases the multiplier by the amount specified by multiplierIncrement
        }
    }

    private IEnumerator EndPerk(){
        while(true){
            yield return new WaitForSeconds(perkDuration);      //after a few seconds
            Destroy(this.gameObject);                           //destroys the cloud
        }
    }
}
