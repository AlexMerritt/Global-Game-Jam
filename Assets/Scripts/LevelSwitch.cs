using UnityEngine;
using System.Collections;

public class LevelSwitch : MonoBehaviour 
{
    public KeyCode Advance;

    public string NextLevel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(Advance))
        {
            SwitchScene();
        }
	}

    public void SwitchScene()
    {
        Application.LoadLevel(NextLevel);
    }
}
