using UnityEngine;
using System.Collections;
using PongClasses;
using System;

public class ShockEffect : Effect {
    float timeLimit;
    float time;
    bool isDone;
    bool enabled;

    public ShockEffect():base()
    {
        time = 0.0f;
        timeLimit = 2.0f;
    }

    public override void Action(GameObject target)
    {
        if (!enabled)
        {
            target.GetComponentInChildren<PaddleScript>().canMove = false;
            enabled = true;
        }
        else if (time >= timeLimit)
        {
            isDone = true;
            target.GetComponentInChildren<PaddleScript>().canMove = true;
        }
        else
            time += Time.deltaTime;
        //Debug.Log("Shock effect is called!!");
    }

    public override bool IsActive()
    {
        return !isDone;
    }
}
