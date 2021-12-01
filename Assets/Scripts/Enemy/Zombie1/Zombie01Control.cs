using SWS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyData
{
    public PathManager path;
}

public class Zombie01Control : EnemyControl
{
    public Zombie01IdleState idleState;
    public Zombie01ChaseState chaseState;
    public Zombie01WalkState walkState;
    public Zombie01AttackState attackState;
    public Zombie01DeadState deadState;

    public Animator animator;
    public override void OnSetup(object data)
    {
        base.OnSetup(data);
        idleState.parent = this;
        chaseState.parent = this;
        walkState.parent = this;
        attackState.parent = this;
        deadState.parent = this;
        GotoState(walkState);
        fieldOfView.getEnemyAction = DefindPlayer;
        
    }

    public override void OnHit(int damage)
    {
        CurrentHeal -= damage;
        if (CurrentHeal <= 0 && !isDead)
        {
            isDead = true;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<FieldOfView>().viewAngle = 0;
            GotoState(deadState);

        }
        base.OnHit(damage);
    }

    public override void SystemUpdate()
    {
        timerCountRof += Time.deltaTime;
        if (isDead)
        {
            callbackStopFindVisiblePlayer();
        }
    }

    public void DefindPlayer(List<Transform> targets, Action callback)
    {
        if (callbackStopFindVisiblePlayer == null)
        {
            callbackStopFindVisiblePlayer = callback;
        }
        //Collider[] hitColliders = Physics.OverlapSphere(trans.position, rangeAttack, playerMask);
        if (targets.Count == 1)
        {
            LookPlayer();
            isWalk = false;
            StopCoroutine("StartFOV");
            fieldOfView.viewMeshFilter.gameObject.SetActive(false);
            float distanceToPlayer = Vector3.Distance(trans.position, PlayerControl.Instance.trans.position);
            if (distanceToPlayer < rangeAttack)
            {
                if (currentState != attackState)
                {
                    GotoState(attackState);
                }
            }
            else
            {
                if (currentState != chaseState)
                {
                    GotoState(chaseState);
                }
            }


        }
        else
        {
            if (!isDead)
            {
                isWalk = true;
                if (currentState != walkState)
                    GotoState(walkState);
            }


        }

    }

    IEnumerator PlayerTakeDamge()
    {
        yield return new WaitForSeconds(0.5f);
        if (!PlayerControl.isDead && !MissionControl.isVictory)
            PlayerControl.Instance.OnPlayerDamage(damge);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, rangeAttack);
    }
}
