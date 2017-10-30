using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDeliverer : MonoBehaviour, ITimable
{

    public float HitDelay = 1;
    public float Damage = 100;

    private PlayerBehaviour _player;
    private TimerBehaviour _timer;
    private int _attackTimerId;
    private EnemyBehaviour _myBehaviour;
    private Animator _animator;
    private AudioSource _audio;
    private bool _attackTriggered = false;

    // Use this for initialization
    void Start () {
        _myBehaviour = GetComponent<EnemyBehaviour>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent <PlayerBehaviour>();
        _timer = GameObject.Find("GameManager").GetComponent<TimerBehaviour>();
        _attackTimerId = _timer.Subscribe(this, HitDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (_myBehaviour.EnemyState == Enums.State.Attack && !_attackTriggered)
        {
            _attackTriggered = true;
            _timer.StartTimer(_attackTimerId);
        } else if (_myBehaviour.EnemyState != Enums.State.Attack && _attackTriggered)
        {
            _attackTriggered = false;
            _timer.StopTimer(_attackTimerId);
        }
	}

    public void TimeUp(int i)
    {
        DeliverHit();
    }

    void DeliverHit()
    {
        _player.ReceiveHit(Damage);
        _myBehaviour.HitDelivered();
        _timer.StartTimer(_attackTimerId);
        PlayAnimation();
        PlaySound();
    }

    void PlayAnimation()
    {
        _animator.SetTrigger("Attack");
    }

    void PlaySound()
    {
        if (!_audio.isPlaying && Random.value > 0.5)
            _audio.Play();
    }

    void OnDestroy()
    {
        _timer.Unsubscribe(_attackTimerId);
    }

}
