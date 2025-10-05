using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{

    //For starting the game
    [SerializeField] GameObject panelToRemove; //Remove this panel once the game is started to avoid overlap
    [SerializeField] Dialolgue_SO intro_Dialogue; //The intro dialogue to the game
    [SerializeField] GameObject barrierToLeave; //Barrier to leave the restaurant to start the game

    public void QuitGame()
    {
        Debug.Log("The game WOULD quit BUTTTTTTTTTTTTT you in editor BITCH");
        Application.Quit();
    }


    public void StartGame()
    {
        Destroy(panelToRemove); //Removes this panel

        //Reads the intro dialogue
        FindFirstObjectByType<Dialogue_Manager>().startDialogue(intro_Dialogue, 0);

        //Talks to the player to starts
        FindFirstObjectByType<PlayerMovement>().startGame();

        Destroy(barrierToLeave, 10); //Destroy after 6 seconds
    }


    public void ResetMainScene()
    {
        SceneManager.LoadScene("Main_Game");
    }

    public void GoToCredit()
    {
        SceneManager.LoadScene("Credits_Scene");
    }

    public void GoToWin()
    {
        Debug.Log("Dont got scene managing here yet so WAIIIIT");
        SceneManager.LoadScene("Win_Scene");
    }
}
