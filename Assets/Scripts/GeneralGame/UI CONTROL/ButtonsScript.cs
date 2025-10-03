using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{

    public int InGameScene, MainMenuScene, CreditsScene, WinScene;

    public void QuitGame()
    {
        Debug.Log("The game WOULD quit BUTTTTTTTTTTTTT you in editor BITCH");
        Application.Quit();
    }


    public void GoToMainGame()
    {
        Debug.Log("Dont got scene managing here yet so WAIIIIT");
    }


    public void GoToMainMenu()
    {
        Debug.Log("Dont got scene managing here yet so WAIIIIT");
    }

    public void GoToCredit()
    {
        Debug.Log("Dont got scene managing here yet so WAIIIIT");
    }

    public void GoToWin()
    {
        Debug.Log("Dont got scene managing here yet so WAIIIIT");
    }
}
