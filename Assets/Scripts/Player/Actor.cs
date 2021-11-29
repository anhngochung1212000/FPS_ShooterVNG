using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int maxHP;
    public System.Action onHPChanged;

    private int curHP;

    public int CurrentHP
    {
        get { return curHP; }
        set
        {
            curHP = value;
            if (onHPChanged != null)
            {
                onHPChanged();
            }
        }
    }

    public bool IsDead()
    {
        return CurrentHP <= 0;
    }

    protected virtual void OnHPChanged(int newValue) { }

    // Use this for initialization
    protected virtual void Start()
    {
        CurrentHP = maxHP;
    }

    protected virtual void Update() { }
    protected virtual void Awake() { }

    public virtual void TakeDamage(int damage)
    {
        if (IsDead()) return;

        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            Die();
        }
    }

    public virtual void Die() { }
}
