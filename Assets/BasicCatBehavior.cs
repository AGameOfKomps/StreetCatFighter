using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCatBehavior : MonoBehaviour, ICatDamageable
{

    public const float DELAY_HIT = 3;

    public float Energy = 100;
    public float Speed = 0.1f;
    public float Damage = 10;
    public GameObject PowerUp;

    private float hitCountdown;
    private bool isOnTarget;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        hitCountdown = DELAY_HIT;
        isOnTarget = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOnTarget())
            DeliverHit();
        else
            Move();
    }

    bool IsOnTarget()
    {
        return isOnTarget;
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
            hitCountdown = DELAY_HIT;
            GetComponent<Animator>().SetBool("Attack", true);
        }
        else
        {
            if (hitCountdown <= 1.5)
                GetComponent<Animator>().SetBool("Attack", false);
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
        Energy -= attackType == PlayerBehaviour.AttackType.Light ? 20
            : attackType == PlayerBehaviour.AttackType.Heavy ? 40
            : attackType == PlayerBehaviour.AttackType.ComboOne ? 45
            : attackType == PlayerBehaviour.AttackType.ComboTwo ? 50
            : 55;
        if (Energy <= 0)
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