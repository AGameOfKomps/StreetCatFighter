using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour
{

    public float Speed;
    public int Health;
    public int Damage;

    public GameObject Player;

    private bool IsOnTarget;

    // Use this for initialization
    void Start()
    {
        Speed = 0.1f;
        Health = 100;
        Damage = 10;
        IsOnTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOnTarget)
        {
            DeliverHit();
        }
        else
        {
            Move();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            IsOnTarget = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            IsOnTarget = false;
        }
    }

    void DeliverHit()
    {
        // deliver hit animation
        Player.GetComponent<PlayerBehaviour>().ReceiveHit(Damage);
    }

    void Move()
    {
        Vector2 distance = Player.transform.position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? distance : delta);
    }

    public void ReceiveHit(bool attackType)
    {
        // receive hit animation
        Health -= attackType ? 20 : 40;
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // death animation
        Destroy(this);
    }
}