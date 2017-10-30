using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public const float MULTIPLIER_DELAY = 2;

    public int HighScore = 0;
    public int ComboHits = 0;
    public int KillCount = 0;

    private float multiplierCountdown;
    private float multiplierElapsed;

    public GameObject Boss;
    public GameObject Score;
    public GameObject Combo;
    public GameObject HitCount;


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
        Score = GameObject.FindGameObjectWithTag("Score");
        Combo = GameObject.FindGameObjectWithTag("Combo");
        HitCount = GameObject.FindGameObjectWithTag("HitCount");

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
        ComboHits++;

        Score.GetComponent<Text>().text = "SCORE: " + HighScore;

        Combo.SetActive(true);
        HitCount.SetActive(true);
        HitCount.GetComponent<Text>().text = ComboHits + "";
    }

    public void registerHitReceive()
    {
        Score = GameObject.FindGameObjectWithTag("Score");
        Combo = GameObject.FindGameObjectWithTag("Combo");
        HitCount = GameObject.FindGameObjectWithTag("HitCount");

        multiplierCountdown = 0;
        multiplierElapsed = 0;
        ComboHits = 0;
        
        Combo.SetActive(true);
        HitCount.SetActive(true);
        HitCount.GetComponent<Text>().text = ComboHits + "";
    }

    public void AddKill()
    {
        KillCount++;
    }
}
