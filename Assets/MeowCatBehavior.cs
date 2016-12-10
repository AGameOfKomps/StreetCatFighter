using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowCatBehavior : MonoBehaviour {

    public const float Speed = 0.2f;
    public const float Damage = 10;
    public const float DelayHit = 0.1f;
    public const float DeadSpace = 3;

    public GameObject Player;
    public GameObject PlantPot;

    private float Energy;
    private float HitCountdown;

    // Use this for initialization
    void Start () {
        Energy = 100;
        HitCountdown = DelayHit;
    }
	
	// Update is called once per frame
	void Update () {
		if (IsOnTarget())
        {
            DeliverHit();
        }
        else
        {
            Move();
        }
	}

    bool IsOnTarget()
    {
        Vector2 distance = Player.transform.position - transform.position;
        return distance.Equals(distance.normalized * DeadSpace);
    }

    void Move()
    {
        Vector2 distance = Player.transform.position - transform.position;
        Vector2 target = distance - distance.normalized * DeadSpace;
        Vector2 delta = target.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > target.sqrMagnitude ? target : delta);
    }

    void DeliverHit()
    {
        if (HitCountdown <= 0)
        {
            SpawnPlantPot();
            HitCountdown = DelayHit;
        }
        else
        {
            HitCountdown -= Time.deltaTime;
        }
    }

    void SpawnPlantPot()
    {
        Vector2 plantPotPosition = new Vector2(Player.transform.position.x, Screen.height / 2);
        Instantiate(PlantPot, plantPotPosition, Quaternion.identity);
    }

    public void ReceiveHit(PlayerBehaviour.AttackType attackType)
    {
        // receive hit animation
        Energy -= PlayerBehaviour.AttackType.Weak.Equals(attackType) ? 20 : 40;
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
