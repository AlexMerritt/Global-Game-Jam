using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour 
{
    public GameObject Player;

    public void SpawnPlayer()
    {
        GameObject g = (GameObject)Instantiate(Player, transform.position, transform.rotation);
        g.name = "Player";
    }
}
