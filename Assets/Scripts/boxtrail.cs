using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxtrail : MonoBehaviour {
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;

    public GameObject echoBox;
    [SerializeField]
    private GameObject player;
    //private PlayerControl playermove;
    private Rigidbody2D rb;
    private bool moving;
	// Use this for initialization
	void Start () {
        rb = player.GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        //if (playermove.moving)
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            moving = true;
        }
        else
            moving = false;
        if (moving)
        {
            if (timeBtwSpawns <= 0)
            {
                GameObject instance = Instantiate(echoBox, transform.position, Quaternion.identity);
                Destroy(instance, 2f);
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}
