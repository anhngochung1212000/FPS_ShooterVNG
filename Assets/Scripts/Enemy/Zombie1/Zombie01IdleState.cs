using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Zombie01IdleState : FSMState
{
    [System.NonSerialized]
    public Zombie01Control parent;
    float value;
    public override void OnEnter()
    {
        Debug.LogError("Idle");
        value = 1;
    }
    public override void OnUpdate()
    {
        if (value > 0)
        {
            value -= Time.deltaTime * 4f;
            parent.animator.SetFloat("Speed", value);

        }
    }
    public override void OnExit()
    {
    }
}
