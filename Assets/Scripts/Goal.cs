using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.collider2D.tag == "Player")
        {
            // Broadcast Player Read Goal
			Application.LoadLevel ("Victory");
        }
    }
}
