using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float Energy;
    public int Lives;

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


    }

    void RecieveHit(int damage)
    {
        Energy -= damage;

        if (Energy <= 0)
            Die();
    }

    void Die()
    {

    }
}
