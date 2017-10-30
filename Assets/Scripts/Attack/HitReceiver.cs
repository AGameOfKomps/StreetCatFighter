using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReceiver : MonoBehaviour, ITimable
{

    public float LightDamage = 20;
    public float HeavyDamage = 40;
    public float ComboOneDamage = 45;
    public float ComboTwoDamage = 50;
    public float ComboThreeDamage = 55;

    public float StunDelay = 1;

    private const float NO_REDUCTION = 1;

    private TimerBehaviour _timer;
    private Mortal _myMortal;
    private EnemyBehaviour _myBehaviour;
    private int _stunTimerId;
    private bool _stunTriggered = false;

    void Start()
    {
        _myMortal = GetComponent<Mortal>();
        _myBehaviour = GetComponent<EnemyBehaviour>();
        _timer = GameObject.Find("GameManager").GetComponent<TimerBehaviour>();
        _stunTimerId = _timer.Subscribe(this, StunDelay);
    }

    void Update()
    {
        if (_myBehaviour.EnemyState == Enums.State.Stun && !_stunTriggered)
        {
            _timer.StartTimer(_stunTimerId);
            _stunTriggered = true;
        }
    }

    public void ReceiveHit(PlayerBehaviour.AttackType attackType)
    {
        float damage = attackType == PlayerBehaviour.AttackType.Light ? LightDamage
            : attackType == PlayerBehaviour.AttackType.Heavy ? HeavyDamage
            : attackType == PlayerBehaviour.AttackType.ComboOne ? ComboOneDamage
            : attackType == PlayerBehaviour.AttackType.ComboTwo ? ComboTwoDamage
            : ComboThreeDamage;
        float damageReduction = GetDamageReduction(attackType);

        if (damageReduction == NO_REDUCTION)
        {
            // receive hit animation
            _myBehaviour.HitReceived();
        }

        _myMortal.Energy -= damage * damageReduction;
    }

    private float GetDamageReduction(PlayerBehaviour.AttackType attackType)
    {
        float result = NO_REDUCTION;
        DamageReductor damageReductor = GetComponent<DamageReductor>();
        if (damageReductor != null)
        {
            result = damageReductor.CalculateDamageReduction(attackType);
        }
        return result;
    }

    public virtual void TimeUp(int id)
    {
        _stunTriggered = false;
        _myBehaviour.WakeUp();
    }

    void OnDestroy()
    {
        _timer.Unsubscribe(_stunTimerId);
    }
}
