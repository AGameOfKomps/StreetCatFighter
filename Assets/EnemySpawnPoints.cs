using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour {


    public GameObject bc;
    public GameObject wc;
    public GameObject mc;
    public GameObject sc;

    int bcCount = 0;
    int wcCount = 0;
    int mcCount = 0;
    int scCount = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (bcCount + wcCount + scCount + mcCount > 2)
            return;

        var x = transform.position.x + (Random.value > 0.5f ? -1 : 1) * Random.Range(0, GetComponent<Renderer>().bounds.size.x);
        var y = transform.position.y + (Random.value > 0.5f ? -1 : 1) * Random.Range(0, GetComponent<Renderer>().bounds.size.y);

        var r = Random.value;
        if (r < 0.33f && bcCount < 2)
        {
            Instantiate(bc, new Vector2(x, y), Quaternion.identity); //BasicCatBehavior
            bcCount++;
        }
        else if (r >= 0.33f && r < 0.4f && wcCount < 1)
        {
            Instantiate(wc, new Vector2(x, y), Quaternion.identity);//MeowCatBehavior;
            wcCount++;
        }
        else if (r >= 0.4f && r < 0.7f && mcCount < 1)
        {
            Instantiate(mc, new Vector2(x, y), Quaternion.identity);//WizardCatBehaviour;
            mcCount++;
        }
        else if (r >= 0.7f && scCount < 1)
        {
            Instantiate(sc, new Vector2(x, y), Quaternion.identity); //ShieldCatBehaviour;
            scCount++;
        }
    }
}
