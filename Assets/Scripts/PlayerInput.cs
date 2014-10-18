using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
    PlayerControl pc;

    public KeyCode jump;

    public KeyCode pauseTime;

    bool paused = false;
	// Use this for initialization
	void Start () 
    {
        pc = GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        /*if (Input.GetKeyDown(abi1))
        {
            pc.ActivateAbility(1);
            //print("Key 1 Down");
        }

        if (Input.GetKeyDown(abi2))
        {
            pc.ActivateAbility(2);
            //print("Key 2 Down");
        }

        if (Input.GetKeyDown(abi3))
        {
            pc.ActivateAbility(3);
            //print("Key 3 Down");
        }

		if(Input.GetKeyDown (abi4))
		{
			pc.ActivateAbility (4);
		}

		if (Input.GetKeyDown (abi5))
		{
			pc.ActivateAbility (5);
		}

		if(Input.GetKeyDown (abi6))
		{
			pc.ActivateAbility (6);
		}*/

        if (Input.GetKeyDown(jump))
        {
            pc.Jump();
        }

        if (Input.GetKeyDown(pauseTime))
        {
            paused = !paused;
            GameObject g = GameObject.Find("Level");
            g.GetComponent<TimeManager>().PauseTime(paused);
        }
	}
}
