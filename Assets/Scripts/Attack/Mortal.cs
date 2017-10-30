using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortal : MonoBehaviour  {

    public float Energy = 100;
    private GameObject _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update () {
        if (Energy <= 0)
            Die();
	}

    void Die()
    {
        _gameManager.GetComponent<ScoreCounter>().AddKill();
        _gameManager.GetComponent<Orbs>().Appear(transform.position);
        Destroy(this.gameObject);
    }
}
