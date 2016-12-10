using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour {

    public int Speed;
    public int Health;
    public GameObject Player;
    
    // Use this for initialization
	void Start () {
        Speed = 1;
        Health = 100;
	}
	
	// Update is called once per frame
	void Update () {
        if (IsOnTarget())
        {
            ReceiveHit(0);
            if (Health > 0)
            {
                DeliverHit();
            }
            else
            {
                Die();
            }
        }
        else
        {
            Move();
        }
	}

    bool IsOnTarget()
    {
        return false;
    }

    void Move()
    {
        Vector2 distance = Player.transform.position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? distance : delta);
    }

    void DeliverHit()
    {

    }

    public void ReceiveHit(int HitPoints)
    {
        Health -= HitPoints;
    }

    void Die()
    {

    }
}
