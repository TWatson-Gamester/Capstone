using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1MoveRestrict : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P2Right"))
        {
            Player1Move.walkLeftP1 = false;
        }
        if (other.gameObject.CompareTag("P2Left"))
        {
            Player1Move.walkRightP1 = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("P2Right"))
        {
            Player1Move.walkLeftP1 = true;
        }
        if (other.gameObject.CompareTag("P2Left"))
        {
            Player1Move.walkRightP1 = true;
        }
    }
}
