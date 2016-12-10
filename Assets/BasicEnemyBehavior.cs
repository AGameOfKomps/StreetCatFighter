using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour
{

    public float Speed;
    public int Health;
    public int Damage;
    public GameObject Player;

    // Use this for initialization
    void Start()
    {
        Speed = 0.1f;
        Health = 100;
        Damage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOnTarget())
        {
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
        // deliver hit animation
        Player.GetComponent<PlayerBehaviour>().ReceiveHit(Damage);
    }

    public void ReceiveHit(bool attackType)
    {
        // receive hit animation
        Health -= attackType ? 20 : 40;
    }

    void Die()
    {
        // death animation
        Destroy(this);
    }
}