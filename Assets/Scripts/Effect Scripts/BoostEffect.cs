using UnityEngine;
using System.Collections;
using PongClasses;
using System;

public class BoostEffect : Effect {
    int boostSpeedBonus;
    float timeLimit;
    float time;
    bool enabled;
    bool isDone;
    GameObject paddle;

    public BoostEffect()
    {
        boostSpeedBonus = 15;
        time = 0.0f;
        timeLimit = 3.5f;
        enabled = false;
        isDone = false;
        paddle = null;
    }

    public override void Action(GameObject target)
    {
        if(!enabled)
        {
            enabled = true;
            paddle = target.transform.GetChild(0).gameObject;
            paddle.GetComponent<PaddleScript>().SpeedBonus = boostSpeedBonus;
            paddle.GetComponent<PaddleScript>().BoostEffectEnabled = true;
            //Debug.Log("Boost effect is called!!");
        }
        else if (time >= timeLimit)
        {
            isDone = true;
            paddle.GetComponent<PaddleScript>().SpeedBonus = 0;
            paddle.GetComponent<PaddleScript>().BoostEffectEnabled = false;
            //Debug.Log("Boost effect is done!!");
        }
        time += Time.deltaTime;
    }

    public override bool IsActive()
    {
        return !isDone;
    }
}
