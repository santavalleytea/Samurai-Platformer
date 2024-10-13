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
        defeatScreen.SetActive(true);
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null) {
            player.enabled = false;
        }
        Time.timeScale = 0;
    }

    public void RestartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
