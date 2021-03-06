﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowCatBehavior : MonoBehaviour, ICatDamageable
{

    public const float DELAY_HIT = 3;
    public const float DEAD_SPACE = 3;

    public float Energy = 100;
    public float Speed = 0.06f;
    public float Damage = 10;
    public GameObject PlantPot;
    public GameObject PowerUp;

    private PlayerBehaviour.PlayerDirection direction;
    private float hitCountdown;
    private GameObject player;
    private bool freeze;

    // Use this for initialization
    void Start () {
        hitCountdown = DELAY_HIT;
        freeze = false;
        direction = PlayerBehaviour.PlayerDirection.Left;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (IsOnTarget())
            DeliverHit();
        else
            Move();
	}

    bool IsOnTarget()
    {
        Vector2 distance = player.transform.position - transform.position;
        return freeze || distance == distance.normalized * DEAD_SPACE;
    }

    void DeliverHit()
    {
        if (hitCountdown <= 0)
        {
            SpawnPlantPot();
            hitCountdown = DELAY_HIT;
            GetComponent<Animator>().SetTrigger("StopMeowing");
            freeze = false;
        }
        else
        {
            if (hitCountdown <= 1.5)
            {
                GetComponent<Animator>().SetTrigger("Meowing");
                freeze = true;
                if (!GetComponent<AudioSource>().isPlaying && Random.value > 0.5)
                    GetComponent<AudioSource>().Play();
            }
            hitCountdown -= Time.deltaTime;
        }
    }

    void SpawnPlantPot()
    {
        Vector2 plantPotPosition = new Vector2(player.transform.position.x, 0.5f);
        GameObject plantPot = Instantiate(PlantPot, plantPotPosition, Quaternion.identity);
        plantPot.GetComponent<PlantPotBehavior>().TargetY = player.transform.position.y;
    }

    void Move()
    {
        Vector2 distance = player.transform.position - transform.position;
        Vector2 target = distance - distance.normalized * DEAD_SPACE;
        Vector2 delta = target.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > target.sqrMagnitude ? target : delta);
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
