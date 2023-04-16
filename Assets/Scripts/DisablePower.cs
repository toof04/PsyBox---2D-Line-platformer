using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePower : MonoBehaviour
{
    public LineCreater lc;
    private Animator anim;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            lc.disablePower();
            anim.SetTrigger("pillTaken");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
