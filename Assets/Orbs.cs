using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour {

    public GameObject EnergyOrb;
    public GameObject PowerOrb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Appear(Vector2 v)
    {
        var e = Random.Range(1, 5);
        for (int i = 0; i < e; i++)
        {
            Instantiate(EnergyOrb, v, Quaternion.identity);
        }

        var p = Random.Range(0, 3);
        for (int i = 0; i < p; i++)
        {
            Instantiate(PowerOrb, v, Quaternion.identity);
        }
    }
}
