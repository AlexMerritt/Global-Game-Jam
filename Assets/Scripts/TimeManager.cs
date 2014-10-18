using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PauseTime(bool pause)
    {
        if (pause)
        {
            print("Time Paused");
            Time.timeScale = 0;
        }
        else
        {
            print("time Resumed");
            Time.timeScale = 1;
        }
    }

    public void UnPauseTime()
    {
        print("Time Resumed");
        Time.timeScale = 1;
    }
}
