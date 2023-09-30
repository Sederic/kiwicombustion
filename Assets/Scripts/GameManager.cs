using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    #region Scene Management
    public void GameScene()
    {
        SceneManager.LoadScene("Level 01 Scene");
    }
    public void LoseGame()
    {
        SceneManager.LoadScene("Loose Scene");
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Win Scene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("Credits Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion


}
