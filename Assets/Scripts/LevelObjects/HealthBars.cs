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
    [SerializeField] Image P1Round1;
    [SerializeField] Image P1Round2;
    [SerializeField] Image P2Round1;
    [SerializeField] Image P2Round2;

    private bool gameRunning = true;

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.Player1Wins == 0)
        {
            P1Round1.enabled = false;
            P1Round2.enabled = false;
        }
        else if (SaveScript.Player1Wins == 1)
        {
            P1Round1.enabled = true;
            P1Round2.enabled = false;
        }
        else
        {
            P1Round1.enabled = true;
            P1Round2.enabled = true;
        }

        if (SaveScript.Player2Wins == 0)
        {
            P2Round1.enabled = false;
            P2Round2.enabled = false;
        }
        else if (SaveScript.Player2Wins == 1)
        {
            P2Round1.enabled = true;
            P2Round2.enabled = false;
        }
        else
        {
            P2Round1.enabled = true;
            P2Round2.enabled = true;
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
                gameRunning = false;
                EndGame();
            }
        }

    }

    public void EndGame()
    {
        StartCoroutine(GameRoundFinish());
    }

    IEnumerator GameRoundFinish()
    {
        yield return new WaitForSeconds(4);
        SaveScript.Player1Health = 1;
        SaveScript.Player2Health = 1;

        if(SaveScript.Player1Wins == 3 || SaveScript.Player2Wins == 3)
        {
            SaveScript.Player1Wins = 0;
            SaveScript.Player2Wins = 0;
            StartCoroutine(GameFinish());
        }
        else
            SceneManager.LoadScene("Level 1");
    }

    IEnumerator GameFinish()
    {
        yield return new WaitForSeconds(4);
        gameRunning = false;

        SceneManager.LoadScene("MainMenu");
    }
}
