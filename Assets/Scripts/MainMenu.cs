using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject controlsMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject creditsMenu;
    [SerializeField] GameObject gameModes;
    [SerializeField] GameObject characterSelect;

    [Header("GameModes")]
    [SerializeField] GameObject player1Picker;
    [SerializeField] GameObject player2Picker;
    [SerializeField] GameObject fightTime;

    private bool isAIMatch;
    private GameObject player1Character;
    private GameObject player2Character;

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
        characterSelect.SetActive(false);
    }

    public void controlsSet()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void optionsSet()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void creditsSet()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void gameModeSet()
    {
        mainMenu.SetActive(false);
        gameModes.SetActive(true);
    }

    public void characterSelectorSet(bool isAIMatch)
    {
        gameModes.SetActive(false);
        characterSelect.SetActive(true);
        this.isAIMatch = isAIMatch;
    }

    public void characterSelectToGameModes()
    {
        characterSelect.SetActive(false);
        gameModes.SetActive(true);
    }

    public void player1Pick(int choice)
    {
        if (isAIMatch) choice++;
        LevelSpawn.Player1Choice = choice;
    }

    public void player1PickCharacter(GameObject character)
    {
        character.SetActive(true);
        player1Character = character;
        player1Picker.SetActive(false);
        player2Picker.SetActive(true);
    }

    public void player2Pick(int choice)
    {
        LevelSpawn.Player2Choice = choice;
    }

    public void player2PickCharacter(GameObject character)
    {
        character.SetActive(true);
        player2Character = character;
        player2Picker.SetActive(false);
        fightTime.SetActive(true);
    }

    public void backOutPlayer1Choice()
    {
        player1Character.SetActive(false);
        player2Picker.SetActive(false);
        player1Picker.SetActive(true);
    }

    public void backOutPlayer2Choice()
    {
        player2Character.SetActive(false);
        fightTime.SetActive(false);
        player2Picker.SetActive(true);
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
