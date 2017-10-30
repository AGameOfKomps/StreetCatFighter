using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour {

    public GameObject EnergyOrb;
    public GameObject PowerOrb;
    public GameObject SpeedOrb;

    public void Appear(Vector2 spawnPoint)
    {
        if (Random.value > 0.7f)
        {
            int powerUp = Random.Range(1, 4);
            if (powerUp == 1)
                Instantiate(EnergyOrb, spawnPoint, Quaternion.identity);
            else if (powerUp == 2)
                Instantiate(PowerOrb, spawnPoint, Quaternion.identity);
            else
                Instantiate(SpeedOrb, spawnPoint, Quaternion.identity);
        }
    }
}
