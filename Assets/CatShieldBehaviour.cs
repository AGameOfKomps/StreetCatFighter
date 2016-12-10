using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatShieldBehaviour : MonoBehaviour {


    public float Speed;
    public float Health;
    public GameObject Player;


    public float DamageByHeavy;
    public float DamageByWeak;
    public bool HasShield = true;
    public float time = 3;

    public float Damage = 5;
    // Use this for initialization
    void Start () {
        Speed = 0.1f;
        Health = 100;
        DamageByHeavy = 5;
        DamageByWeak = 2;
    }
	
	// Update is called once per frame
	void Update () {
        if (IsOnTarget())
        {
            //ReceiveHit(0);
            if (Health > 0)
            {
                DeliverHit();
            }
            else
            {
                Die();
            }

            
            time -= Time.deltaTime;
            if (time < 0)
                HasShield = true;
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
        Player.GetComponent<PlayerBehaviour>().ReceiveHit(Damage);
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
