using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Zombie01WalkState : FSMState
{
    [System.NonSerialized]
    public Zombie01Control parent;
    float value;
    public override void OnEnter()
    {
        parent.StartCoroutine("StartFOV");
        if (!parent.moveNavi.isStart)
            parent.moveNavi.StartMove();
        else
            parent.moveNavi.Resume();
        parent.animator.SetFloat("Speed", 1);
    }
    public override void OnUpdate()
    {
    }
    public override void OnExit()
    {
        parent.moveNavi.Pause();
        parent.animator.SetFloat("Speed", 0);
    }
}
