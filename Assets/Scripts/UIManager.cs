using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=Ay159WsGDJQ

public class UIManager : MonoBehaviour
{
    public int playerLives;
    public int playerScore;

    //for UI lives 
    public GameObject[] livesRemaining;

    public void UpdatePlayerLives(int playerLives) {
        this.playerLives = playerLives;
    }

    private void Update() {
        if (playerLives < 0) {
            Destroy(livesRemaining[0].gameObject);
        }

        for (int i = 0; i < livesRemaining.Length; i++) {
            if (i >= playerLives) {
                Destroy(livesRemaining[i].gameObject);
            }
        }

        Console.Out.WriteLine(playerLives);
    }

    public void LoseLife() {
        playerLives--;
    }

    public void updateScore(int s) {
        playerScore += s;
    }
}
