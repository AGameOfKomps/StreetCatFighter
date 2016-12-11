using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPotBehavior : MonoBehaviour {

    public float Speed = 0.5f;
    public float Damage = 20;
    public float TargetY = 0;

    private bool isOnTarget;
    private GameObject player;
    
    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        isOnTarget = false;
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
        return isOnTarget;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isOnTarget = true;
        }
    }

    void DeliverHit()
    {
        player.GetComponent<PlayerBehaviour>().ReceiveHit(Damage);
        Die();
    }

    void Move()
    {
        transform.Translate(Vector2.down * Speed);
        if (transform.position.y <= TargetY)
        {
            Die();
        }
    }

    void Die()
    {
        DestroyImmediate(this.gameObject, true);
    }
}
