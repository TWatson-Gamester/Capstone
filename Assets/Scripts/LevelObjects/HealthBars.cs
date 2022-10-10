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

    private bool gameRunning = true;

    // Update is called once per frame
    void Update()
    {
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
                EndGame();
            }
        }

    }

    public void EndGame()
    {
        StartCoroutine(GameFinish());
    }

    IEnumerator GameFinish()
    {
        yield return new WaitForSeconds(4);
        gameRunning = false;
        SaveScript.Player1Health = 1;
        SaveScript.Player2Health = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
