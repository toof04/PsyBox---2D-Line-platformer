using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleStatus : MonoBehaviour
{
    public int health;
    private Rigidbody2D rb;
    // Use this for initialization
    void Awake()
    {
        health = 100;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            KillPlayer();
        }

    }
    public void Freeze(bool freeze)
    {
        if (freeze == true)
        {
            rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionX;
            gameObject.layer = 0;
            

        }
        else
        {
            gameObject.layer = 13;
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

        }
    }
    void KillPlayer()
    {
        Destroy(gameObject);
    }
}
