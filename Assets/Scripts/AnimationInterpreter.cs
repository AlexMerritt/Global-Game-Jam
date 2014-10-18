using UnityEngine;
using System.Collections;

public class AnimationInterpreter : MonoBehaviour 
{
    public PlayerControl pc;

    public void Step(int step)
    {
        pc.OnStep(step);
    }
}