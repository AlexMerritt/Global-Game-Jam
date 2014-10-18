using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
    public KeyCode SpawnPlayer;
    bool playerSpawned = false;

    public bool IsPlayerSpawned()
    {
        return playerSpawned;
    }
	// Use this for initialization
	void Start () {
	
		if (!playerSpawned)
		{
			GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>().SpawnPlayer();
			playerSpawned = true;
			
			// Enable Camera
		}

	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(SpawnPlayer))
       // {
           // if (!playerSpawned)
           // {
               // GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>().SpawnPlayer();
               // playerSpawned = true;

                // Enable Camera
            //}
       //}
	}

    void SetCameraPosition()
    {
    }

    public void ReachGoal(string goal)
    {
    }
}
