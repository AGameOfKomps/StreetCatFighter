using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCatBehavior : MonoBehaviour, ICatDamageable
{

    public const float Speed = 0.1f;
    public const float Damage = 10;
    public const float DelayHit = 3;

    public GameObject Player;

    private float Energy;
    private float HitCountdown;
    private bool IsOnTarget;

    // Use this for initialization
    void Start()
    {
        Energy = 100;
        HitCountdown = DelayHit;
        IsOnTarget = false;
        Player = GameObject.FindGameObjectWithTag("Player");
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
        if (HitCountdown <= 0)
        {
            Player.GetComponent<PlayerBehaviour>().ReceiveHit(Damage);
            HitCountdown = DelayHit;
        }
        else
        {
            HitCountdown -= Time.deltaTime;
        }
    }

    void Move()
    {
        Vector2 distance = Player.transform.position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? distance : delta);
    }

    public void ReceiveHit(PlayerBehaviour.AttackType attackType)
    {
        // receive hit animation
        Energy -= PlayerBehaviour.AttackType.Weak == attackType ? 20 : 40;
        if (Energy <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // death animation
        Destroy(this.gameObject);
    }
}