using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<TMP_Text> texts = new List<TMP_Text>();
    int index = 0;

    public void nextText()
    {
        if (texts.Count > 0)
        {
            if (index == texts.Count)
            {
                GameScene();
            }
            texts[index].gameObject.SetActive(true);
            index++;
            
        }
    }



    #region Scene Management
    public void StoryScene()
    {
        SceneManager.LoadScene("Story Scene");
    }
    public void GameScene()
    {
        SceneManager.LoadScene("Level 01 Scene");
    }
    public void LoseGame()
    {
        SceneManager.LoadScene("Lose Scene");
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
