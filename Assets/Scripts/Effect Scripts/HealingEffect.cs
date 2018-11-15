using UnityEngine;
using System.Collections;
using PongClasses;
using System;

public class HealingEffect : Effect
{
    int healingPoints = 2;
    bool isDone = false;


    public override void Action(GameObject target)
    {
        //Debug.Log("Healing Effect is Called!!");
        target.GetComponent<PlayerController>().player.AddHP(healingPoints);
        //Debug.Log("Healing Effect is Done!! Added " + healingPoints + " healing points");
        isDone = true;
    }

    public override bool IsActive()
    {
        return !isDone;
    }
}
