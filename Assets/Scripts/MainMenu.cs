using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject controlsMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject creditsMenu;
    [SerializeField] GameObject gameModes;
    [SerializeField] GameObject versusMode;
    [SerializeField] GameObject AIMode;

    public void LoadGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenuSet()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        gameModes.SetActive(false);
        versusMode.SetActive(false);
        AIMode.SetActive(false);
    }
}
