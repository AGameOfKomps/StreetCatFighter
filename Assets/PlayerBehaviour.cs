using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float Energy = 100;
    public int Lives = 3;
    public float Speed = 1;

    public enum AttackType
    {
        Weak,
        Heavy
    }
    
    void Start()
    {

    }
    
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(x, y) * Speed);
    }

    public void ReceiveHit(float damage)
    {
        Energy -= damage;

        if (Energy <= 0)
            Die();
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
