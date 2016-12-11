using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {

    public const float MULTIPLIER_DELAY = 2;

    public int HighScore = 0;
    public int KillCount = 0;

    private float multiplierCountdown;
    private float multiplierElapsed;

    public GameObject Boss;

    // Use this for initialization
    void Start () {
        Boss = GameObject.FindGameObjectWithTag("Boss");
        multiplierCountdown = MULTIPLIER_DELAY;
        multiplierElapsed = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (multiplierCountdown > 0)
        {
            multiplierCountdown -= Time.deltaTime;
            multiplierElapsed += Time.deltaTime;
        }
        else
        {
            multiplierCountdown = 0;
            multiplierElapsed = 0;

            Debug.Log("HighScore multiplier expired " + HighScore);
        }

        if (KillCount > 20)
            Boss.GetComponent<BossBehaviour>().Spawn();
	}

    public void registerHitDeliver(PlayerBehaviour.AttackType attackType)
    {
        multiplierCountdown = MULTIPLIER_DELAY;
        int score = attackType == PlayerBehaviour.AttackType.Light ? 200
            : attackType == PlayerBehaviour.AttackType.Heavy ? 500
            : attackType == PlayerBehaviour.AttackType.ComboOne ? 1000
            : attackType == PlayerBehaviour.AttackType.ComboTwo ? 1100
            : attackType == PlayerBehaviour.AttackType.ComboThree ? 1200
            : 1500;
        if (multiplierElapsed > 0)
            score *= Mathf.FloorToInt(multiplierElapsed * 0.1f);

        HighScore += score;

        Debug.Log("New HighScore! " + HighScore);
    }

    public void registerHitReceive()
    {
        multiplierCountdown = 0;
        multiplierElapsed = 0;

        Debug.Log("HighScore multiplier cut " + HighScore);
    }

    public void AddKill()
    {
        KillCount++;
    }
}
