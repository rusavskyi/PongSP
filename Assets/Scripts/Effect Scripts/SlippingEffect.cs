using UnityEngine;
using System.Collections;
using PongClasses;
using System;

public class SlippingEffect : Effect {


    bool enabled;
    bool isDone;
    float timeLimit;
    float time;

    public SlippingEffect()
    {
        enabled = false;
        isDone = false;
        timeLimit = 5f;
        time = 0.0f;
    }

    public override void Action(GameObject target)
    {
        if (!enabled)
        {
            target.transform.GetComponentInChildren<PaddleScript>().SlippingEffectEnabled = true;
            target.transform.GetComponentInChildren<PaddleScript>().moveS = PaddleScript.moveState.stay;
            enabled = true;
            //Debug.Log("Slipping Effect is called!");
        }
        else if (time >= timeLimit)
        {
            target.transform.GetComponentInChildren<PaddleScript>().SlippingEffectEnabled = false;
            isDone = true;
            //Debug.Log("Slipping Effect is done!");
        }
        time += Time.deltaTime;
    }

    public override bool IsActive()
    {
        return !isDone;
    }

}
