using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawn : MonoBehaviour
{
    public GameObject Player1Character;
    public GameObject Player2Character;
    public Transform Player1Spawn;
    public Transform Player2Spawn;
    void Start()
    {
        Instantiate(Player1Character, Player1Spawn.position, Player1Spawn.rotation);
        Instantiate(Player2Character, Player2Spawn.position, Player2Spawn.rotation);
    }
}
