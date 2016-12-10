using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatShieldBehaviour : MonoBehaviour, ICatDamageable
{


    public const float Speed =0.1f;
    public const float DelayHit = 3;
    public const float DamageByHeavy = 5;
    public const float DamageByWeak = 2;
    public const float Damage = 5;

    public GameObject Player;
    public float Health = 100;
    public bool HasShield = true;
    public float time = 3;
    
    private bool IsOnTarget = false;

    private float HitCountdown = DelayHit;
    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (IsOnTarget)
        {
            DeliverHit();
        }
        else
        {
            Move();
        }


        time -= Time.deltaTime;
            if (time < 0)
                HasShield = true;
        
        
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

    void Move()
    {
        Vector2 distance = Player.transform.position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? distance : delta);
    }

    void DeliverHit()
    {
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

    public void ReceiveHit(PlayerBehaviour.AttackType attType)
    {
        if(attType == PlayerBehaviour.AttackType.Heavy)
        {
            if (HasShield)
            {
                Health -= DamageByHeavy * 0.3f;
                HasShield = false;
            }

            else
                Health -= DamageByHeavy;
        }
        else
        {
            if (HasShield)
                Health -= DamageByWeak * 0.3f;

            else
                Health -= DamageByWeak;
        }
        
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
