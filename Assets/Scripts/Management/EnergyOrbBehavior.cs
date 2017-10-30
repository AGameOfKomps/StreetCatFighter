using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrbBehavior : AbstractOrbBehavior
{
    public float EnergyBoost = 20;

    public override void Consume()
    {
        player.GetComponent<PlayerBehaviour>().Energy += EnergyBoost;
        Destroy(this.gameObject);
    }
}
