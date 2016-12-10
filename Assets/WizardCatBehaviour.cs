using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCatBehaviour : MonoBehaviour
{
    public const float DamageByHeavy = 5;
    public const float DamageByWeak = 2;
    

    public GameObject Player;
    public GameObject Laser;
    public float Speed=0.1f;

    public float Health = 100;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector2.Distance(transform.position, Player.transform.position);
        if (distance < 7.5f)
        {
            MoveAway();
            Laser.SetActive(false);

        }
        else if (distance > 8.5f)
        {
            Move();
            Laser.SetActive(false);

        }
        else
        {
            if (Random.value > 0.75f)
            {
                Laser.SetActive(true);
                GetComponentInChildren<LaserBehaviour>().Point();
            }
        }

        
                
    }

    void Move()
    {
        Vector2 distance = Player.transform.position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? distance : delta);
    }

    void MoveAway()
    {
        Vector2 distance = Player.transform.position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? -distance : -delta);
    }

   
    public void ReceiveHit(PlayerBehaviour.AttackType attType)
    {
        if (attType == PlayerBehaviour.AttackType.Heavy)
            Health -= DamageByHeavy;
        
        else
            Health -= DamageByWeak;


        if (Health <= 0)
            Die();
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}