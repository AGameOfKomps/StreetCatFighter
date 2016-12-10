using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowCatBehavior : MonoBehaviour, ICatDamageable
{

    public const float Speed = 0.2f;
    public const float Damage = 10;
    public const float DelayHit = 3;
    public const float DeadSpace = 3;

    public GameObject PlantPot;

    private float energy;
    private float hitCountdown;
    private GameObject player;

    // Use this for initialization
    void Start () {
        energy = 100;
        hitCountdown = DelayHit;
        player = GameObject.FindGameObjectWithTag("Player");
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
        Vector2 distance = player.transform.position - transform.position;
        return distance == distance.normalized * DeadSpace;
    }

    void Move()
    {
        Vector2 distance = player.transform.position - transform.position;
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
            GetComponent<Animator>().SetTrigger("StopMeowing");
        }
        else
        {
            if (hitCountdown <= 1.5)
                GetComponent<Animator>().SetTrigger("Meowing");
            hitCountdown -= Time.deltaTime;
        }
    }

    void SpawnPlantPot()
    {
        Vector2 plantPotPosition = new Vector2(player.transform.position.x, Camera.main.orthographicSize);
        GameObject plantPot = Instantiate(PlantPot, plantPotPosition, Quaternion.identity);
        plantPot.GetComponent<PlantPotBehavior>().TargetY = player.transform.position.y;
    }

    public void ReceiveHit(PlayerBehaviour.AttackType attackType)
    {
        // receive hit animation
        energy -= attackType == PlayerBehaviour.AttackType.Light ? 20
            : attackType == PlayerBehaviour.AttackType.Heavy ? 40
            : attackType == PlayerBehaviour.AttackType.ComboOne ? 45
            : attackType == PlayerBehaviour.AttackType.ComboTwo ? 50
            : 55;
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
