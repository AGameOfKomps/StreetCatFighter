using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICatDamageable
{
    void ReceiveHit(PlayerBehaviour.AttackType attType);
}
