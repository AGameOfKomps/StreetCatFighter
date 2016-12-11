using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public const float INITIAL_SPEED = 0.5f;

    public float Energy = 100;
    public int Lives = 3;
    public float Speed = INITIAL_SPEED;

    public bool LaserOn;
    public PlayerDirection Direction = PlayerDirection.Right;

    public Rigidbody2D RigidBody;
    public int EnergyPowerUpCount = 0;
    public float GodModeCountdown = 0;
    public float SuperSpeedCountdown = 0;

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
    private bool G;

    private bool _inAir;

    public enum AttackType
    {
        None,
        Light,
        Heavy,
        ComboOne,
        ComboTwo,
        ComboThree,
        ComboFour
    }

    public GameObject LightAttackHitBox;
    public GameObject HeavyAttackHitBox;
    public GameObject ComboOneAttackHitBox;
    public GameObject ComboTwoAttackHitBox;
    public GameObject ComboThreeAttackHitBox;
    public GameObject ComboFourAttackHitBox;
    public GameObject GameManager;

    private float _oldY;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        GameManager.GetComponent<ScoreCounter>().HighScore = 0;
    }

    void Update()
    {
        if (LaserOn)
        {
            GoToLaser();
            return;
        }

        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");

        A = Input.GetButtonDown("Light Attack");
        B = Input.GetButtonDown("Heavy Attack");

        J = Input.GetButtonDown("Jump");

        G = Input.GetButton("Block");

        if (H > 0)
            Direction = PlayerDirection.Right;
        else if (H < 0)
            Direction = PlayerDirection.Left;

        if (!A && !B && !G)
        {
            GetComponent<Animator>().SetFloat("Horizontal", H);
            GetComponent<Animator>().SetFloat("Vertical", V);
            transform.Translate(new Vector2(H, !_inAir ? V * 0.5f : 0) * Speed);
        }

        if (!_inAir && J)
        {
            GetComponent<Animator>().SetTrigger("Jump");
            _oldY = transform.position.y;
            Jump();
        }

        if (_inAir && _oldY > transform.position.y)
        {
            GetComponent<Animator>().SetTrigger("Land");
            RigidBody.gravityScale = 0;
            RigidBody.velocity = Vector2.zero;
            _inAir = false;
        }

        if (!_inAir && A)
        {
            GetComponent<Animator>().SetTrigger("L");
            TriggerAttack(AttackType.Light);
        }

        if (!_inAir && B)
        {
            GetComponent<Animator>().SetTrigger("H");
            TriggerAttack(AttackType.Heavy);
        }

        GetComponent<Animator>().SetBool("Block", G);

        if (G && H != 0)
        {
            GetComponent<Animator>().SetFloat("Horizontal", H);
            GetComponent<Animator>().SetFloat("Vertical", V);
            DodgeMove();
        }

        TriggerAttack(GetComponent<Combos>().FetchCombo(V, H, B, A, J, false));

        if (GodModeCountdown > 0)
            GodModeCountdown -= Time.deltaTime;

        if (SuperSpeedCountdown > 0)
            SuperSpeedCountdown -= Time.deltaTime;
        else
            Speed = INITIAL_SPEED;
    }

    private void DodgeMove()
    {
        var position = new Vector3();

        if (H > 0)
            position.x = transform.position.x + 3;
        else if (H < 0)
            position.x = transform.position.x - 3;
        else
            position.x = transform.position.x;

        if (V > 0)
            position.y = transform.position.y + 2.5f;
        else if (H < 0)
            position.y = transform.position.y - 2.5f;
        else
            position.y = transform.position.y;

        Vector2 distance = position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? distance : delta);
    }

    private void GoToLaser()
    {
        GameObject Laser = GameObject.FindGameObjectWithTag("Laser");
        Vector2 distance = Laser.transform.position - transform.position;
        Vector2 delta = distance.normalized * Speed;
        transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? distance : delta);
    }

    private void TriggerAttack(AttackType attackType)
    {
        if (attackType == AttackType.Light)
            PerformAttack(LightAttackHitBox, 0.5f);
        else if (attackType == AttackType.Heavy)
            PerformAttack(HeavyAttackHitBox, 0.5f);
        else if (attackType == AttackType.ComboOne)
            PerformAttack(ComboOneAttackHitBox, 1);
        else if (attackType == AttackType.ComboTwo)
            PerformAttack(ComboTwoAttackHitBox, 1);
        else if (attackType == AttackType.ComboThree)
            PerformAttack(ComboThreeAttackHitBox, 1);
        else if (attackType == AttackType.ComboFour)
            PerformAttack(ComboFourAttackHitBox, 1);
    }

    private void PerformAttack(GameObject hitBox, float deltaX)
    {
        hitBox.SetActive(true);

        // Start animation

        var x = transform.position.x;
        var y = transform.position.y;
        if (Direction == PlayerDirection.Right)
            hitBox.transform.position = new Vector2(x + deltaX, y);
        else if (Direction == PlayerDirection.Left)
            hitBox.transform.position = new Vector2(x - deltaX, y);
    }

    public void ReceiveHit(float damage)
    {
        if (GodModeCountdown <= 0)
        {
            Energy -= G ? damage * 0.7f : damage;
            GameManager.GetComponent<ScoreCounter>().registerHitReceive();

            if (Energy <= 0)
                Die();
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("EnergyOrb"))
            collider.gameObject.GetComponent<EnergyOrbBehavior>().Consume();
        else if (collider.tag.Equals("PowerOrb"))
            collider.gameObject.GetComponent<PowerOrbBehavior>().Consume();
        else if (collider.tag.Equals("SpeedOrb"))
            collider.gameObject.GetComponent<SpeedOrbBehavior>().Consume();
    }

    void Jump()
    {
        RigidBody.gravityScale = 1.2f;
        _inAir = true;
        RigidBody.velocity = Vector2.up * 5;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
