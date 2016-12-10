using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float Energy = 100;
    public int Lives = 3;
    public float Speed = 1;

    public PlayerDirection Direction = PlayerDirection.Right;

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

    public GameObject WeakAttackCollider;
    public GameObject HeavyAttackCollider;

    void Start()
    {

    }
    
    void Update()
    {
        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");

        A = Input.GetButtonDown("Weak Attack");
        B = Input.GetButtonDown("Heavy Attack");

        J = Input.GetButtonDown("Jump");

        if (H > 0)
            Direction = PlayerDirection.Right;
        else if (H < 0)
            Direction = PlayerDirection.Left;

        if (!A || !B)
            transform.Translate(new Vector2(H, V) * Speed);

        if (J)
            Jump();

        if (!J && A)
            WeakAttack();

        if (!J && B)
            HeavyAttack();
    }

    private void WeakAttack()
    {
        WeakAttackCollider.SetActive(true);

        // Start animation

        if (Direction == PlayerDirection.Right)
            WeakAttackCollider.transform.position = new Vector2(-0.5f, 0);
        else if (Direction == PlayerDirection.Left)
            WeakAttackCollider.transform.position = new Vector2(0.5f, 0);

        // Do damage
        WeakAttackCollider.SetActive(false);
    }

    private void HeavyAttack()
    {
        WeakAttackCollider.SetActive(true);

        // Start animation

        if (Direction == PlayerDirection.Right)
            WeakAttackCollider.transform.position = new Vector2(-0.5f, 0);
        else if (Direction == PlayerDirection.Left)
            WeakAttackCollider.transform.position = new Vector2(0.5f, 0);

        // Do damage
        WeakAttackCollider.SetActive(false);
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
