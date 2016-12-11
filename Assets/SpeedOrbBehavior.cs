using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedOrbBehavior : AbstractOrbBehavior
{
    public float SpeedBoost = 0.5f;
    public float SpeedBoostDuration = 3;

    public override void Consume()
    {
        player.GetComponent<PlayerBehaviour>().Speed += SpeedBoost;
        player.GetComponent<PlayerBehaviour>().SuperSpeedCountdown = SpeedBoostDuration;
        // animacija
        Destroy(this.gameObject);
    }
}
