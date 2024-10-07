using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {
    public GameObject defeatScreen;
    public Button restartButton;
    public Button quitButton;

    public void Start() {
        defeatScreen.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }
    public void Defeat() {
        Debug.Log("Game Over Screen Activated");
        defeatScreen.SetActive(true);
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null) {
            player.enabled = false;
        }
    }

    public void RestartGame() {
        Debug.Log("Clicked");
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        SceneManager.LoadScene(1);
    }
}
