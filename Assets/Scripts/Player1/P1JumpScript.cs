using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1JumpScript : MonoBehaviour
{
    public GameObject Player1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P2SpaceDectector"))
        {
            if (Player1Move.FacingRight)
            {
                Player1.transform.Translate(.8f, 0, 0);
            }
            if (Player1Move.FacingLeft)
            {
                Player1.transform.Translate(-.8f, 0, 0);
            }
        }
    }
}
