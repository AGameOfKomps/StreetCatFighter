using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    private GameObject player;
    private bool chasePlayer;
    
    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        chasePlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (chasePlayer && transform.position != player.transform.position)
        {
            Vector2 distance = player.transform.position - transform.position;
            Vector2 delta = distance.normalized * player.GetComponent<PlayerBehaviour>().Speed;
            transform.Translate(delta.sqrMagnitude > distance.sqrMagnitude ? distance : delta);

            if (transform.position.y >= 0.5f)
                transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

            if (transform.position.y <= -0.5f)
                transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);

            if (transform.position.x <= -14f)
                transform.position = new Vector3(-14f, transform.position.y, transform.position.z);

            if (transform.position.x >= 14f)
                transform.position = new Vector3(14f, transform.position.y, transform.position.z);
        }
        else
        {
            chasePlayer = false;
        }
	}

    public void triggerChase()
    {
        chasePlayer = true;
    }
}
