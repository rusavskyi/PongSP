using UnityEngine;
using System.Collections;
using PongClasses;
using System;

public class ShieldEffect : Effect //Need graphics
{
    bool enabled;
    bool isDone;
    float timeLimit;
    float time;
    GameObject gate;

    public ShieldEffect() : base()
    {
        enabled = false;
        isDone = false;
        timeLimit = 3.5f;
        time = 0.0f;
        gate = null;
    }

    public override void Action(GameObject target)
    {

        //enable GateBlocker;
        if (!enabled)
        {
            Transform tmp = target.transform.GetChild(1);
            if (tmp != null && tmp.gameObject != null)
                gate = tmp.gameObject;
            else Debug.Log("Fail to get Gate pointer!!!");
            gate.GetComponent<MeshRenderer>().enabled = true;

            target.GetComponentInChildren<BoxCollider>().isTrigger = false;
            enabled = true;
        }
        else if (time >= timeLimit)
        {
            target.GetComponentInChildren<BoxCollider>().isTrigger = true;
            gate.GetComponent<MeshRenderer>().enabled = false;
            isDone = true;
        }
        time += Time.deltaTime;
    }

    public override bool IsActive()
    {
        return !isDone;
    }
}
