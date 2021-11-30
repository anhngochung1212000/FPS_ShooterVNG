using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Zombie01AttackState : FSMState
{
    [System.NonSerialized]
    public Zombie01Control parent;
    public override void OnEnter()
    {

    }
   
    public override void OnUpdate()
    {
        if (parent.timerCountRof > parent.rof && !PlayerControl.instance.isDead)
        {
            //parent.audioSource.PlayOneShot(parent.attackSound);
            parent.timerCountRof = 0;
            parent.animator.SetTrigger("Attack");
            parent.StartCoroutine("PlayerTakeDamge");
           
        }
    }
    public override void OnExit()
    {
    }
}
