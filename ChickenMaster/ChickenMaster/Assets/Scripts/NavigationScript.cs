using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene(Constants.MenuScene);
    }

    public void GoToHowToPlay()
    {
        SceneManager.LoadScene(Constants.HowToPlayScene);
    }


    public void GoToCredits()
    {
        SceneManager.LoadScene(Constants.CreditsScene);
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene(Constants.Level1Scene);
    }


    public void Exit()
    {
        Application.Quit();
    }

}
