using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float Energy = 100;
    public float Speed = 0.07f;

    [HideInInspector]
    public Enums.State EnemyState;
    [HideInInspector]
    public PlayerBehaviour.PlayerDirection EnemyDirection;

    private bool _hitReceived = false;
    private bool _hitDelivered = false;
    private bool _onTarget = false;
    private bool _wakeUp = false;

    // Use this for initialization
    void Start () {
        EnemyState = Enums.State.Idle;
        EnemyDirection = PlayerBehaviour.PlayerDirection.Right;
    }

    // Update is called once per frame
    void Update() {
        if (IsToStun())
        {
            EnemyState = Enums.State.Stun;
        }
        else if (EnemyState == Enums.State.Stun && IsWakeUp())
        {
            EnemyState = Enums.State.Idle;
        }
        else if (EnemyState == Enums.State.Idle && IsToMove())
        {
            EnemyState = Enums.State.Move;
        }
        else if (EnemyState == Enums.State.Move && IsToAttack())
        {
            EnemyState = Enums.State.Attack;
        }
        else if (EnemyState == Enums.State.Attack && IsToIdle())
        {
            EnemyState = Enums.State.Idle;
        }
    }

    private bool IsToStun()
    {
        return IsTriggered(ref _hitReceived);
    }

    private bool IsWakeUp()
    {
        return IsTriggered(ref _wakeUp);
    }

    private bool IsToMove()
    {
        return true;
    }

    private bool IsToAttack()
    {
        return IsTriggered(ref _onTarget);
    }

    private bool IsToIdle()
    {
        return /*!IsTriggered(ref _onTarget) ||*/ IsTriggered(ref _hitDelivered);
    }
     
    public void HitReceived()
    {
        _hitReceived = true;
    }

    public void HitDelivered()
    {
        _hitDelivered = true;
    }

    public void OnTarget()
    {
        _onTarget = true;
    }

    public void WakeUp()
    {
        _wakeUp = true;
    }

    private bool IsTriggered(ref bool flag)
    {
        bool result = flag;
        flag = false;
        return result;
    }
}
