using UnityEngine;
using System.Collections;
using PongClasses;
using System;

public class FissionEffect : Effect
{
    bool enabled;
    bool isDone;
    float time;
    float timeLimit;
    GameObject ball;


    public FissionEffect()
    {
        ball = GameObject.Find("Arena").GetComponent<Game>().ballPrefab;
        enabled = false;
        isDone = false;
        time = 0.0f;
        timeLimit = 3.5f;
    }

    public override void Action(GameObject target)
    {
        if (!enabled)
        {
            enabled = true;
            target.GetComponentInChildren<PaddleScript>().FissionEffectEnabled = true;
            target.GetComponentInChildren<PaddleScript>().BallPrefab = ball;
            //Debug.Log("Fission effect is called!!");
        }
        else if (time >= timeLimit)
        {
            isDone = true;
            target.GetComponentInChildren<PaddleScript>().FissionEffectEnabled = false;
            target.GetComponentInChildren<PaddleScript>().BallPrefab = null;
        }
        time += Time.deltaTime;
    }

    public override bool IsActive()
    {
        return !isDone;
    }
}
