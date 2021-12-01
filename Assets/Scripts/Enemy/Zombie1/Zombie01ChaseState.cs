using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Zombie01ChaseState : FSMState
{
    [System.NonSerialized]
    public Zombie01Control parent;
    public override void OnEnter()
    {
        parent.animator.SetFloat("Speed", 1);
    }
    public override void OnUpdate()
    {
        if (!parent.isAttack && !PlayerControl.isDead && !MissionControl.isVictory)
        {
            //parent.trans.Translate(Vector3.forward * parent.moveNavi.agent.speed * Time.deltaTime);
            parent.moveNavi.agent.isStopped = false;
            parent.moveNavi.agent.SetDestination(PlayerControl.Instance.trans.position);
        }

    }
    public override void OnExit()
    {
        parent.animator.SetFloat("Speed", 0);
        parent.moveNavi.agent.isStopped = true;
    }
}
