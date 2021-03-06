﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCatBehaviour : MonoBehaviour, ICatDamageable
{
    public const float DELAY_HIT = 3;
    public const float DELAY_SHIELD_RESTORE = 3;

    public float Energy = 100;
    public float Speed = 0.07f;
    public float Damage = 5;
    public GameObject PowerUp;

    private PlayerBehaviour.PlayerDirection direction;
    private float hitCountdown;
    private float shieldRestoreCountdown;
    private bool isOnTarget;
    private bool hasShield;
    private GameObject player;

    // Use this for initialization
    void Start () {
        hitCountdown = DELAY_HIT;
        shieldRestoreCountdown = DELAY_SHIELD_RESTORE;
        isOnTarget = false;
        hasShield = true;
        direction = PlayerBehaviour.PlayerDirection.Left;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (IsOnTarget())
            DeliverHit();
        else
            Move();

        RestoreShield();
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

    void Move()
    {
        Vector2 distance = player.transform.position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? distance : delta);
        AdjustDirection();
    }

    void AdjustDirection()
    {
        if (transform.position.x >= player.transform.position.x && direction == PlayerBehaviour.PlayerDirection.Right)
        {
            GetComponent<Animator>().SetTrigger("GoLeft");
            direction = PlayerBehaviour.PlayerDirection.Left;
        }
        else if (transform.position.x < player.transform.position.x && direction == PlayerBehaviour.PlayerDirection.Left)
        {
            GetComponent<Animator>().SetTrigger("GoRight");
            direction = PlayerBehaviour.PlayerDirection.Right;
        }
    }

    void DeliverHit()
    {
        if (hitCountdown <= 0)
        {
            player.GetComponent<PlayerBehaviour>().ReceiveHit(Damage);
            hitCountdown = DELAY_HIT;
            GetComponent<Animator>().SetTrigger("Attack");
            if (!GetComponent<AudioSource>().isPlaying && Random.value > 0.5)
                    GetComponent<AudioSource>().Play();
        }
        else
        {
            if (hitCountdown <= 1.5)
                GetComponent<Animator>().SetTrigger("Stop");
            hitCountdown -= Time.deltaTime;
        }
    }

    void RestoreShield()
    {
        if (!hasShield)
        {
            if (shieldRestoreCountdown <= 0)
            {
                GetComponent<Animator>().SetTrigger("RegainedShield");
                hasShield = true;
            }
            else
            {
                shieldRestoreCountdown -= Time.deltaTime;
            }
        }
    }

    public void ReceiveHit(PlayerBehaviour.AttackType attackType)
    {
        Energy -= attackType == PlayerBehaviour.AttackType.Light ? applyDamageReductor(2, false)
            : attackType == PlayerBehaviour.AttackType.Heavy ? applyDamageReductor(5, true)
            : attackType == PlayerBehaviour.AttackType.ComboOne ? applyDamageReductor(7, true)
            : attackType == PlayerBehaviour.AttackType.ComboTwo ? applyDamageReductor(10, true)
            : applyDamageReductor(12, true);
        if (Energy <= 0)
        {
            Die();
        }
    }

    float applyDamageReductor(float damage, bool isShieldBreaker)
    {
        float result = damage;
        if (hasShield)
        {
            result = damage * 0.3f;
            if (isShieldBreaker)
            {
                hasShield = false;
                GetComponent<Animator>().SetTrigger("LostShield");
            }
        }
        return result;
    }

    void Die()
    {
        PowerUp.GetComponent<Orbs>().Appear(transform.position);
        Destroy(this.gameObject);
    }
}
