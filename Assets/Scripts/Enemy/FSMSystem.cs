using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem : MonoBehaviour
{
    private List<FSMState> arrState = new List<FSMState>();
    public FSMState currentState;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void GotoState(FSMState state)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        // arrState.Add(state);
        currentState = state;
        currentState.OnEnter();
    }
    public void GotoState(FSMState newState, object data)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter(data);
    }
    private void FixedUpdate()
    {
        OnSystemFixedUpdate();
        if (currentState != null)
        {
            currentState.OnFixedUpdate();
        }
    }

    private void LateUpdate()
    {
        OnSystemLateUpdate();
        if (currentState != null)
        {
            currentState.OnLateUpdate();
        }
    }

    private void Update()
    {
        SystemUpdate();
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }
    public virtual void SystemUpdate()
    {

    }
    public virtual void OnSystemFixedUpdate()
    {

    }
    public virtual void OnSystemLateUpdate()
    {

    }
    public virtual void EnemyBecameVisible()
    {

    }
    public virtual void EnemyBecameInvisible()
    {

    }
    public void OnBecameInvisible()
    {
       
    }
    public void OnBecameVisible()
    {
        //Hide
        EnemyBecameVisible();
    }
}
