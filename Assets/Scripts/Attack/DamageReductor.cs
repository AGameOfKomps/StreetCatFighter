using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReductor : MonoBehaviour, ITimable
{

    public float ShieldProtection = 0.1f;
    public float ShieldRestoreDelay = 1;

    private const float NO_REDUCTION = 1;

    private TimerBehaviour _timer;
    private int _shieldRestoreTimerId;
    private bool _hasShield = true;

    void Start()
    {
        _timer = GameObject.Find("GameManager").GetComponent<TimerBehaviour>();
        _shieldRestoreTimerId = _timer.Subscribe(this, ShieldRestoreDelay);
    }

    public float CalculateDamageReduction(PlayerBehaviour.AttackType attackType)
    {
        float result = NO_REDUCTION;
        if (_hasShield)
        {
            if (PlayerBehaviour.AttackType.Light != attackType)
            {
                _hasShield = false;
                _timer.StartTimer(_shieldRestoreTimerId);
            }
            else
            {
                result = ShieldProtection;
            }
        }
        return result;
    }

    public void TimeUp(int id)
    {
        _hasShield = true;
    }
}
