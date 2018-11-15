using UnityEngine;
using System.Collections;
using PongClasses;
using System;

public class PaddleStretchEffect : Effect

{

    float timeLimit;
    float time;
    bool enabled;
    bool isDone;
    GameObject paddle;

    public PaddleStretchEffect()
    {
        time = 0.0f;
        timeLimit = 5f;
        enabled = false;
        isDone = false;
        paddle = null;
    }

    public override void Action(GameObject target)
    {
        if (!enabled)
        {
            enabled = true;
            target.GetComponentInChildren<PaddleScript>().changePuddleSize(PaddleScript.typeOfResize.stretch);
            //Debug.Log("Stretch effect is called!!");
        }
        else if (time >= timeLimit)
        {
            isDone = true;
            target.GetComponentInChildren<PaddleScript>().changePuddleSize(PaddleScript.typeOfResize.original);
            //Debug.Log("Stretch effect is done!!");
        }
        time += Time.deltaTime;
    }

    public override bool IsActive()
    {
        return !isDone;
    }

}
