﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour
{

    public const float DELAY_HIT = 3;
    public const float DELAY_SHIELD_RESTORE = 3;

    public float Energy = 500;
    public float Speed = 0.05f;
    public float Damage = 5;
    public GameObject PowerUp;

    public bool Spawned = false;

    private PlayerBehaviour.PlayerDirection direction;
    private float hitCountdown;
    private float shieldRestoreCountdown;
    private bool isOnTarget;
    private bool hasShield;
    private GameObject player;

    public GameObject Text;
    public GameObject Line;
    public GameObject Bar;
    public GameObject Canvas;

    void Start()
    {
        hitCountdown = DELAY_HIT;
        shieldRestoreCountdown = DELAY_SHIELD_RESTORE;
        isOnTarget = false;
        hasShield = true;
        direction = PlayerBehaviour.PlayerDirection.Left;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (!Spawned)
            return;

        if (IsOnTarget())
            DeliverHit();
        else
            Move();
    }

    public void Spawn()
    {
        Spawned = true;

        Text.SetActive(true);
        Line.SetActive(true);
        Bar.SetActive(true);
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
            GetComponent<AudioSource>().Play();
            player.GetComponent<PlayerBehaviour>().ReceiveHit(Damage);
            hitCountdown = DELAY_HIT;
            GetComponent<Animator>().SetBool("Attack", true);
            if (!GetComponent<AudioSource>().isPlaying && Random.value > 0.5)
                GetComponent<AudioSource>().Play();
        }
        else
        {
            if (hitCountdown <= 1.5)
                GetComponent<Animator>().SetBool("Attack", false);
            hitCountdown -= Time.deltaTime;
        }
    }

    public void ReceiveHit(PlayerBehaviour.AttackType attackType)
    {
        Energy -= attackType == PlayerBehaviour.AttackType.Light ? applyDamageReductor(2, false)
            : attackType == PlayerBehaviour.AttackType.Heavy ? applyDamageReductor(5, false)
            : attackType == PlayerBehaviour.AttackType.ComboOne ? applyDamageReductor(7, false)
            : attackType == PlayerBehaviour.AttackType.ComboTwo ? applyDamageReductor(10, false)
            : applyDamageReductor(12, true);

        RectTransform rt = Bar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(Energy, 16.7f);

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
        SceneManager.LoadScene("MainMenu");
        Destroy(this.gameObject);
    }

}
