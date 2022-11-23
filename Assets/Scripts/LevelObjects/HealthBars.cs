using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    [SerializeField] Image P1Green;
    [SerializeField] Image P2Green;
    [SerializeField] Image P1Red;
    [SerializeField] Image P2Red;
    [SerializeField] GameObject P1Round1;
    [SerializeField] GameObject P1Round2;
    [SerializeField] GameObject P2Round1;
    [SerializeField] GameObject P2Round2;

    [SerializeField] GameObject Round1Text;
    [SerializeField] GameObject Round2Text;
    [SerializeField] GameObject Round3Text;
    [SerializeField] GameObject Round4Text;
    [SerializeField] GameObject Round5Text;
    [SerializeField] GameObject FightText;
    [SerializeField] GameObject Player1WinText;
    [SerializeField] GameObject Player1LoseText;

    [SerializeField] AudioSource audioController;
    [SerializeField] AudioClip Round1Audio;
    [SerializeField] AudioClip Round2Audio;
    [SerializeField] AudioClip Round3Audio;
    [SerializeField] AudioClip Round4Audio;
    [SerializeField] AudioClip Round5Audio;
    [SerializeField] AudioClip FightAudio;
    [SerializeField] AudioClip PlayerWins;
    [SerializeField] AudioClip PlayerLost;

    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;

    private bool gameRunning = true;

    // Update is called once per frame
    private void Update()
    {
        if (SaveScript.Player1Wins == 0)
        {
            P1Round1.SetActive(false);
            P1Round2.SetActive(false);
        }
        else if (SaveScript.Player1Wins == 1)
        {
            P1Round1.SetActive(true);
            P1Round2.SetActive(false);
        }
        else
        {
            P1Round1.SetActive(true);
            P1Round2.SetActive(true);
        }

        if (SaveScript.Player2Wins == 0)
        {
            P2Round1.SetActive(false);
            P2Round2.SetActive(false);
        }
        else if (SaveScript.Player2Wins == 1)
        {
            P2Round1.SetActive(true);
            P2Round2.SetActive(false);
        }
        else
        {
            P2Round1.SetActive(true);
            P2Round2.SetActive(true);
        }

        if (gameRunning)
        {
            P1Green.fillAmount = SaveScript.Player1Health;
            P2Green.fillAmount = SaveScript.Player2Health;

            if (SaveScript.Player2Timer > 0)
            {
                SaveScript.Player2Timer -= 2f * Time.deltaTime;
            }
            else
            {
                if (P2Red.fillAmount > SaveScript.Player2Health)
                {
                    P2Red.fillAmount -= .003f;
                }
            }

            if (SaveScript.Player1Timer > 0)
            {
                SaveScript.Player1Timer -= 2f * Time.deltaTime;
            }
            else
            {
                if (P1Red.fillAmount > SaveScript.Player1Health)
                {
                    P1Red.fillAmount -= .003f;
                }
            }

            if (SaveScript.Player1Health <= 0 || SaveScript.Player2Health <= 0)
            {
                if (SaveScript.Player1Health <= 0) SaveScript.Player2Wins++;
                else if (SaveScript.Player2Health <= 0) SaveScript.Player1Wins++;
                EndGame();
            }
        }

    }

    private void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(RoundStart());
    }

    public void EndGame()
    {
        gameRunning = false;
        StartCoroutine(GameRoundFinish());
    }

    IEnumerator GameRoundFinish()
    {
        yield return new WaitForSeconds(4);
        SaveScript.Player1Health = 1;
        SaveScript.Player2Health = 1;
        SaveScript.RoundCount++;

        if (SaveScript.Player1Wins == 3 || SaveScript.Player2Wins == 3) StartCoroutine(GameFinish());
        else
        {
            Destroy(Player1);
            Destroy(Player2);
            SceneManager.LoadScene("Level 1");
        }
    }

    IEnumerator GameFinish()
    {
        if (SaveScript.Player1Wins == 3)
        {
            audioController.clip = PlayerWins;
            Player1WinText.SetActive(true);
        }
        else
        {
            audioController.clip = PlayerLost;
            Player1LoseText.SetActive(true);
        }
        audioController.Play();
        SaveScript.Player1Wins = 0;
        SaveScript.Player2Wins = 0;
        SaveScript.RoundCount = 1;
        yield return new WaitForSeconds(4);
        gameRunning = false;

        Destroy(Player1);
        Destroy(Player2);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator RoundStart()
    {
        yield return new WaitForSecondsRealtime(1f);
        switch (SaveScript.RoundCount)
        {
            case 1:
                Round1Text.SetActive(true);
                audioController.clip = Round1Audio;
                audioController.Play();
                break;
            case 2:
                Round2Text.SetActive(true);
                audioController.clip = Round2Audio;
                audioController.Play();
                break;
            case 3:
                Round3Text.SetActive(true);
                audioController.clip = Round3Audio;
                audioController.Play();
                break;
            case 4:
                Round4Text.SetActive(true);
                audioController.clip = Round4Audio;
                audioController.Play();
                break;
            case 5:
                Round5Text.SetActive(true);
                audioController.clip = Round5Audio;
                audioController.Play();
                break;
        }
        yield return new WaitForSecondsRealtime(1f);

        FightText.SetActive(true);
        audioController.clip = FightAudio;
        audioController.Play();

        yield return new WaitForSecondsRealtime(1.5f);

        Round1Text.SetActive(false);
        Round2Text.SetActive(false);
        Round3Text.SetActive(false);
        Round4Text.SetActive(false);
        Round5Text.SetActive(false);
        FightText.SetActive(false);
        Time.timeScale = 1;
    }
}
