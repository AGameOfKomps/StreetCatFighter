using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPotBehavior : MonoBehaviour {

    public const float Speed = 0.5f;
    public const float Damage = 20;

    public GameObject Player;

    private bool isOnTarget;
    private Vector2 target;
    
    // Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        target = Player.transform.position;
        isOnTarget = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isOnTarget)
        {
            DeliverHit();
        }
        else
        {
            Move();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isOnTarget = true;
        }
    }

    void Move()
    {
        transform.Translate(Vector2.down * Speed);
        if (transform.position.Equals(target))
        {
            Die();
        }
    }

    void DeliverHit()
    {
        Player.GetComponent<PlayerBehaviour>().ReceiveHit(Damage);
        Die();
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
