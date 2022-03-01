using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
 public void RestartGame () {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
}
 public void MainMenuLoad () {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
}

public void QuitGame () {
    Application.Quit();
}
}
