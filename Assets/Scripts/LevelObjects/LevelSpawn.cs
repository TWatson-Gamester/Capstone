using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawn : MonoBehaviour
{
    public GameObject P1YBot;
    public GameObject AIYBot1;
    public GameObject AIYBot2;

    public static int Player1Choice;
    public static int Player2Choice;

    public Transform Player1Spawn;
    public Transform Player2Spawn;
    void Start()
    {
        switch (Player1Choice)
        {
            case 1:
                Instantiate(P1YBot, Player1Spawn.position, Player1Spawn.rotation);
                break;
            case 2:
                Instantiate(AIYBot1, Player1Spawn.position, Player2Spawn.rotation);
                break;
        }
        switch (Player2Choice)
        {
            case 1:
                //Instantiate(P2YBot, Player2Spawn.position, Player1Spawn.rotation);
                break;
            case 2:
                Instantiate(AIYBot2, Player2Spawn.position, Player2Spawn.rotation);
                break;
        }
    }
}
