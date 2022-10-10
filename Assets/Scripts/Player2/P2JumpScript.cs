using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2JumpScript : MonoBehaviour
{
    public GameObject Player2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P1SpaceDectector"))
        {
            if (Player2Move.FacingRightP2)
            {
                Player2.transform.Translate(.8f, 0, 0);
            }
            if (Player2Move.FacingLeftP2)
            {
                Player2.transform.Translate(-.8f, 0, 0);
            }
        }
    }
}
