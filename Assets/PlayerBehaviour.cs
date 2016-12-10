using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float Energy = 100;
    public int Lives = 3;
    public float Speed = 1;

    public bool LaserOn;
    public PlayerDirection Direction = PlayerDirection.Right;

    public Rigidbody2D RigidBody;

    public enum PlayerDirection
    {
        Left,
        Right
    }

    // Keys
    private float H;
    private float V;
    private bool A;
    private bool B;
    private bool J;

    public enum AttackType
    {
        Weak,
        Heavy
    }

    public GameObject WeakAttackHitBox;
    public GameObject HeavyAttackHitBox;

    void Start()
    {
    }
    
    void Update()
    {
        if(LaserOn)
        {
            GoToLaser();
            return;
        }
        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");

        A = Input.GetButtonDown("Weak Attack");
        B = Input.GetButtonDown("Heavy Attack");

        J = Input.GetButtonDown("Jump");

        if (H > 0)
            Direction = PlayerDirection.Right;
        else if (H < 0)
            Direction = PlayerDirection.Left;

        if (!A && !B)
        {
            GetComponent<Animator>().SetFloat("Horizontal", H);
            GetComponent<Animator>().SetFloat("Vertical", V);
            transform.Translate(new Vector2(H, V) * Speed);
        }

        if (J)
            Jump();

        if (!J && A)
            WeakAttack();

        if (!J && B)
            HeavyAttack();
    }

    private void GoToLaser()
    {
        GameObject Laser = GameObject.FindGameObjectWithTag("Laser");
        Vector2 distance = Laser.transform.position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? distance : delta);
    }

    private void WeakAttack()
    {
        WeakAttackHitBox.SetActive(true);

        // Start animation

        var x = transform.position.x;
        var y = transform.position.y;
        if (Direction == PlayerDirection.Right)
            WeakAttackHitBox.transform.position = new Vector2(x + 0.5f, y);
        else if (Direction == PlayerDirection.Left)
            WeakAttackHitBox.transform.position = new Vector2(x - 0.5f, y);

        // Do damage
        //WeakAttackHitBox.SetActive(false);
    }

    private void HeavyAttack()
    {
        WeakAttackHitBox.SetActive(true);

        // Start animation

        var x = transform.position.x;
        var y = transform.position.y;
        if (Direction == PlayerDirection.Right)
            WeakAttackHitBox.transform.position = new Vector2(x + 0.5f, y);
        else if (Direction == PlayerDirection.Left)
            WeakAttackHitBox.transform.position = new Vector2(x - 0.5f, y);

        // Do damage
        //WeakAttackHitBox.SetActive(false);
    }

    public void ReceiveHit(float damage)
    {
        Energy -= damage;

        if (Energy <= 0)
            Die();
    }

    void Jump()
    {

    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
