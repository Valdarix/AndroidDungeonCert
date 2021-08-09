using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
   public void StartButton()
   {
       SceneManager.LoadScene(1);
   }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void MenuButton()
    {
        //Load the menu scene when we have one. 
    }
}
