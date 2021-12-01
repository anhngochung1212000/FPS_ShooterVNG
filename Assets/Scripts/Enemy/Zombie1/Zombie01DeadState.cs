using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Zombie01DeadState : FSMState
{
    [System.NonSerialized]
    public Zombie01Control parent;
    public override void OnEnter()
    {
        parent.animator.SetTrigger("Dead");
        parent.EnemyOnDead(true);
        MissionControl.CountEnemy++;
    }
}
