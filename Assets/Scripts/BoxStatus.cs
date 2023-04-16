using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxStatus : MonoBehaviour {
    public int health;
    private Rigidbody2D rb;
    [SerializeField] private GameObject deathParticle;
	// Use this for initialization
	void Awake() {
        health = 100;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (health < 0)
        {
            KillPlayer();
        }
                    
	}
    public void Freeze(bool freeze)
    {
        if (freeze == true)
        {
            rb.constraints = rb.constraints| RigidbodyConstraints2D.FreezePositionX;
            gameObject.transform.GetChild(0).gameObject.layer = 0;
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.layer = 11;
            rb.constraints = RigidbodyConstraints2D.None| RigidbodyConstraints2D.FreezeRotation;

        }
    }
    void KillPlayer()
    {
        GameObject deathP = Instantiate(deathParticle,transform.position,Quaternion.identity);
        Destroy(deathP, 2f);
        Destroy(gameObject);
    }
}
