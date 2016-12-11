using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

    public int PlayerOffset = 2;
    public float LaserDuration = 2;

    private float laserCountdown;
    private GameObject player;

    void Update()
    {
        if (laserCountdown <= 0)
        {
            this.gameObject.SetActive(false);
            player.GetComponent<PlayerBehaviour>().LaserOn = false;
        }
        else
        {
            laserCountdown -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    public void Point () {
        laserCountdown = LaserDuration;
        player = GameObject.FindGameObjectWithTag("Player");
        float r = Random.Range(0, 1);
        if (r < 0.25f)
            transform.position = new Vector2(player.transform.position.x - PlayerOffset, player.transform.position.y - PlayerOffset);
        else if(r >= 0.25f && r < 0.5f)
            transform.position = new Vector2(player.transform.position.x - PlayerOffset, player.transform.position.y + PlayerOffset);
        else if (r >= 0.5f && r < 0.75f)
            transform.position = new Vector2(player.transform.position.x + PlayerOffset, player.transform.position.y - PlayerOffset);
        else if(r >= 0.75f)
            transform.position = new Vector2(player.transform.position.x + PlayerOffset, player.transform.position.y + PlayerOffset);

        player.GetComponent<PlayerBehaviour>().LaserOn = true;
    }


    
}
