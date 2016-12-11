using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractOrbBehavior : MonoBehaviour {

    protected GameObject player;

    private bool inAir;
    private float startingY;
    private Rigidbody2D rigidBody;

    // Use this for initialization
    void Start()
    {
        inAir = true;
        startingY = transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 1.2f;
        rigidBody.velocity = Vector2.up * 3;
        rigidBody.AddForce((transform.position - player.transform.position).normalized * 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (inAir && startingY > transform.position.y)
        {
            inAir = false;
            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.zero;
        }
    }

    public abstract void Consume();
}
