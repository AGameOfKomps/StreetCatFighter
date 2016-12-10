using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public PlayerBehaviour.AttackType AttackType;
    public float Damage;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Enemy"))
        {
            col.GetComponent<ICatDamageable>().ReceiveHit(AttackType);
            this.gameObject.SetActive(false);
        }
    }
}
