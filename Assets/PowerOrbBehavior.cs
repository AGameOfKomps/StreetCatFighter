using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOrbBehavior : AbstractOrbBehavior
{
    public int GodModeThreshold = 5;
    public float GodModeDuration = 5;

    public override void Consume()
    {
        if (++player.GetComponent<PlayerBehaviour>().EnergyPowerUpCount == GodModeThreshold)
        {
            player.GetComponent<PlayerBehaviour>().EnergyPowerUpCount = 0;
            player.GetComponent<PlayerBehaviour>().GodModeCountdown = GodModeDuration;
        }
        // animacija
        Destroy(this.gameObject);
    }
}
