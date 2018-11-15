using UnityEngine;
using System.Collections;
using PongClasses;
using System;

public class ConfusionEffect : Effect
{
    float timeLimit;
    float time;
    bool enabled;
    bool isDone;
    GameObject paddle;

    public ConfusionEffect()
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
            paddle = target.transform.GetChild(0).gameObject;
            paddle.GetComponent<PaddleScript>().reverseControl = true;
            //Debug.Log("Confusion effect is called!!");
        }
        else if (time >= timeLimit)
        {
            isDone = true;
            paddle.GetComponent<PaddleScript>().reverseControl = false;
            //Debug.Log("Confusion effect is done!!");
        }
        time += Time.deltaTime;
    }

    public override bool IsActive()
    {
        return !isDone;
    }
}
