using UnityEngine;
using System.Collections;
using PongClasses;
using System;

public class PaddleSqueezeEffect : Effect {
    //Ефект змінює розмір закетки на менший

    float timeLimit;
    float time;
    bool enabled;
    bool isDone;
    GameObject paddle;

    public PaddleSqueezeEffect()
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
            target.GetComponentInChildren<PaddleScript>().changePuddleSize(PaddleScript.typeOfResize.squeeze);
            //Debug.Log("Squeeze effect is called!!");
        }
        else if (time >= timeLimit)
        {
            isDone = true;
            target.GetComponentInChildren<PaddleScript>().changePuddleSize(PaddleScript.typeOfResize.original);
            //Debug.Log("Squeeze effect is done!!");
        }
        time += Time.deltaTime;
    }

    public override bool IsActive()
    {
        return !isDone;
    }
}
