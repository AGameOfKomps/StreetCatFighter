using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCatBehaviour : MonoBehaviour, ICatDamageable
{
    public const float DELAY_HIT = 2;
    public const float DEAD_SPACE = 3;

    public float Energy = 100;
    public float Speed = 0.1f;
    public GameObject Laser;
    public GameObject PowerUp;

    private float hitCountdown;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        hitCountdown = DELAY_HIT;
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
        return Vector2.Distance(transform.position, player.transform.position) == DEAD_SPACE;
    }

    void DeliverHit()
    {
        if (hitCountdown <= 0)
        {
            Laser.SetActive(true);
            GetComponentInChildren<LaserBehaviour>().Point();
            hitCountdown = DELAY_HIT;
            GetComponent<Animator>().SetTrigger("MoveLaser");
            if (!GetComponent<AudioSource>().isPlaying && Random.value > 0.5)
                    GetComponent<AudioSource>().Play();
        }
        else
        {
            if (hitCountdown <= 1.5)
                GetComponent<Animator>().SetTrigger("Laser");
            hitCountdown -= Time.deltaTime;
        }
    }

    void Move()
    {
        if (Laser.activeSelf) return;
        Vector2 distance = player.transform.position - transform.position;
        Vector2 target = distance - distance.normalized * DEAD_SPACE;
        Vector2 delta = target.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > target.sqrMagnitude ? target : delta);
    }

   
    public void ReceiveHit(PlayerBehaviour.AttackType attackType)
    {
        Energy -= attackType == PlayerBehaviour.AttackType.Light ? 2
            : attackType == PlayerBehaviour.AttackType.Heavy ? 5
            : attackType == PlayerBehaviour.AttackType.ComboOne ? 7
            : attackType == PlayerBehaviour.AttackType.ComboTwo ? 10
            : 12;
        if (Energy <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PowerUp.GetComponent<ScoreCounter>().AddKill();
        PowerUp.GetComponent<Orbs>().Appear(transform.position);
        Destroy(this.gameObject);
    }
}