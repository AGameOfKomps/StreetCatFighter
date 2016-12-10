using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCatBehaviour : MonoBehaviour, ICatDamageable
{
    public float Energy = 100;
    public float Speed = 0.1f;
    public GameObject Laser;
    public GameObject PowerUp;

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
   
    // Update is called once per frame
    void Update()
    {
        var distance = Vector2.Distance(transform.position, player.transform.position);
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
                GetComponentInChildren<LaserBehaviour>().Point();;       
            }
        }         
    }
  


    void Move()
    {
        Vector2 distance = player.transform.position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? distance : delta);
    }

    void MoveAway()
    {
        Vector2 distance = player.transform.position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? -distance : -delta);
    }

   
    public void ReceiveHit(PlayerBehaviour.AttackType attackType)
    {
        Energy -= attackType == PlayerBehaviour.AttackType.Light ? 2
            : attackType == PlayerBehaviour.AttackType.Heavy ? 5
            : attackType == PlayerBehaviour.AttackType.ComboOne ? 7
            : attackType == PlayerBehaviour.AttackType.ComboTwo ? 10
            : 12;
        if (Energy <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PowerUp.GetComponent<Orbs>().Appear(transform.position);
        Destroy(this.gameObject);
    }
}