using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

    public GameObject Player;
    public int PlayerOffset = 2;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	public void Point () {
        float r = Random.Range(0, 1);
        if (r < 0.25f)
            transform.position = new Vector2( Player.transform.position.x - PlayerOffset, Player.transform.position.y - PlayerOffset);
        else if(r>=0.25f && r<0.5f)
            transform.position = new Vector2(Player.transform.position.x - PlayerOffset, Player.transform.position.y + PlayerOffset);
        else if (r>=0.5f && r<0.75f)
            transform.position = new Vector2(Player.transform.position.x + PlayerOffset, Player.transform.position.y - PlayerOffset);
        else if(r>=0.75f)
            transform.position = new Vector2(Player.transform.position.x + PlayerOffset, Player.transform.position.y + PlayerOffset);

        Player.GetComponent<PlayerBehaviour>().LaserOn = true;
    }


    
}
