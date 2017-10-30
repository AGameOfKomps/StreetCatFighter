using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBehaviour : MonoBehaviour {

    public float DeadSpace = 1.5f;

    private GameObject _player;
    private EnemyBehaviour _myBehaviour;
    private Animator _animator;
    
    // Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        _myBehaviour = GetComponent<EnemyBehaviour>();
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		Orientate();
        if (_myBehaviour.EnemyState == Enums.State.Move)
        {
            Move();
        }
	}

    void Orientate()
    {
        if (transform.position.x >= _player.transform.position.x && _myBehaviour.EnemyDirection == PlayerBehaviour.PlayerDirection.Right)
        {
            _animator.SetTrigger("GoLeft");
            _myBehaviour.EnemyDirection = PlayerBehaviour.PlayerDirection.Left;
        }
        else if (transform.position.x < _player.transform.position.x && _myBehaviour.EnemyDirection == PlayerBehaviour.PlayerDirection.Left)
        {
            _animator.SetTrigger("GoRight");
            _myBehaviour.EnemyDirection = PlayerBehaviour.PlayerDirection.Right;
        }
    }

    void Move()
    {
        Vector2 distance = _player.transform.position - transform.position;
        Vector2 target = distance - distance.normalized * DeadSpace;
        Vector2 delta = target.normalized * _myBehaviour.Speed;
        transform.Translate(delta.sqrMagnitude > target.sqrMagnitude ? target : delta);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _myBehaviour.OnTarget();
    }
}
