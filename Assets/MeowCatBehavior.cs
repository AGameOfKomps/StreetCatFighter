using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowCatBehavior : MonoBehaviour {

    public const float Speed = 0.2f;
    public const float Damage = 10;
    public const float DelayHit = 3;
    public const float DeadSpace = 3;

    public GameObject Player;
    public GameObject PlantPot;

    private float energy;
    private float hitCountdown;

    // Use this for initialization
    void Start () {
        energy = 100;
        hitCountdown = DelayHit;
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
        return distance == distance.normalized * DeadSpace;
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
        if (hitCountdown <= 0)
        {
            SpawnPlantPot();
            hitCountdown = DelayHit;
        }
        else
        {
            hitCountdown -= Time.deltaTime;
        }
    }

    void SpawnPlantPot()
    {
        Vector2 plantPotPosition = new Vector2(Player.transform.position.x, Camera.main.orthographicSize);
        GameObject plantPot = Instantiate(PlantPot, plantPotPosition, Quaternion.identity);
        plantPot.GetComponent<PlantPotBehavior>().TargetY = Player.transform.position.y;
    }

    public void ReceiveHit(PlayerBehaviour.AttackType attackType)
    {
        // receive hit animation
        energy -= PlayerBehaviour.AttackType.Weak.Equals(attackType) ? 20 : 40;
        if (energy <= 0)
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
