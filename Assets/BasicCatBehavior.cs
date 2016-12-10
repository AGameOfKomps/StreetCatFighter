using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCatBehavior : MonoBehaviour, ICatDamageable
{

    public const float Speed = 0.1f;
    public const float Damage = 10;
    public const float DelayHit = 3;

    private float energy;
    private float hitCountdown;
    private bool isOnTarget;
    private GameObject player;
    public GameObject PowerUp;

    // Use this for initialization
    void Start()
    {
        energy = 100;
        hitCountdown = DelayHit;
        isOnTarget = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnTarget)
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
            isOnTarget = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isOnTarget = false;
        }
    }

    void DeliverHit()
    {
        // deliver hit animation
        if (hitCountdown <= 0)
        {
            player.GetComponent<PlayerBehaviour>().ReceiveHit(Damage);
            hitCountdown = DelayHit;
        }
        else
        {
            hitCountdown -= Time.deltaTime;
        }
    }

    void Move()
    {
        Vector2 distance = player.transform.position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? distance : delta);
    }

    public void ReceiveHit(PlayerBehaviour.AttackType attackType)
    {
        // receive hit animation
        energy -= PlayerBehaviour.AttackType.Weak == attackType ? 20 : 40;
        if (energy <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // death animation
        PowerUp.GetComponent<Orbs>().Appear(transform.position);
        Destroy(this.gameObject);
    }
}