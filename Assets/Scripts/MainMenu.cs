using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://www.youtube.com/watch?v=DX7HyN7oJjE : start, quit

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadSceneAsync(1); //load scene 1 - index of SampleScene (access through file > build settings > scense in build) 
    }

    public void QuitGame() {
        Application.Quit();
    }
}
