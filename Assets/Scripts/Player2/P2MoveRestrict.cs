using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2MoveRestrict : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P1Left"))
        {
            Player2Move.walkLeft = false;
        }
        if (other.gameObject.CompareTag("P1Right"))
        {
            Player2Move.walkRight = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("P1Left"))
        {
            Player2Move.walkLeft = true;
        }
        if (other.gameObject.CompareTag("P1Right"))
        {
            Player2Move.walkRight = true;
        }
    }
}
