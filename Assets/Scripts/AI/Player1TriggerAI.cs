using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1TriggerAI : MonoBehaviour
{
    [SerializeField] Collider col;
    [SerializeField] float DamageAmt = .1f;
    private ParticleSystem particleSystem;
    public bool emitEffects = false;
    public string ParticleType = "P11";
    private GameObject ChosenParticles;
    public bool isPlayer1 = false;

    private void Start()
    {
        ChosenParticles = GameObject.Find(ParticleType);
        particleSystem = ChosenParticles.gameObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (isPlayer1)
        {
            if (!Player1Actions.Hits)
            {
                col.enabled = true;
            }
            else
            {
                col.enabled = false;
            }
        }
/*        else
        {
            if (!Player2ActionsAI.HitsAI)
            {
                col.enabled = true;
            }
            else
            {
                col.enabled = false;
            }
        }*/

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer1)
        {
/*            if (other.gameObject.CompareTag("Player2"))
            {
                if (emitEffects)
                {
                    particleSystem.Play();
                    Time.timeScale = 0.7f;
                }
                Player1Actions.Hits = true;
                SaveScript.Player2Health -= DamageAmt;
                SaveScript.Player2Timer = 2f;
            }*/
        }
        else
        {
            if (other.gameObject.CompareTag("Player1"))
            {
                if (emitEffects)
                {
                    particleSystem.Play();
                    //Time.timeScale = 0.7f;
                }
                Player2ActionsAI.HitsAI = true;
                SaveScript.Player1Health -= DamageAmt;
                SaveScript.Player1Timer = 2f;
            }
        }
    }
}
