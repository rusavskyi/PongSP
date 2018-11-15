using UnityEngine;
using System.Collections;
using PongClasses;
using System;

public class ReflectorEffect : Effect
{
    float time;
    float timeLimit;
    bool isDone;
    bool enabled;

    public ReflectorEffect()
    {
        time = 0.0f;
        timeLimit = 13.5f;
        isDone = false;
        enabled = false;
    }

    public override void Action(GameObject target)
    {
        if(!enabled)
        {
            target.GetComponentInChildren<PaddleScript>().ReflectorEffectEnabled = true;
            target.GetComponentInChildren<PaddleScript>().playerId = target.GetComponent<PlayerController>().playerID;
            target.GetComponentInChildren<PaddleScript>().paddleTransform = target.transform.GetChild(0).transform;
            enabled = true;
            //Debug.Log("Reflector effect is called!!");
        }
        else if(time >= timeLimit)
        {
            target.GetComponentInChildren<PaddleScript>().ReflectorEffectEnabled = false;
            target.GetComponentInChildren<PaddleScript>().paddleTransform = null;
            isDone = true;
            //Debug.Log("Reflector effect is ended!!");
        }
        time += Time.deltaTime;
    }

    public override bool IsActive()
    {
        return !isDone;
    }
}

