using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableBackground : MonoBehaviour
{
    private Animator anim;
    [SerializeField]private Animator anim2;
    private bool once=false;
    [SerializeField]private bool noTrigger = false;
    [SerializeField] private string animationName;
    private void Start()
    {
        if(!noTrigger)GetComponent<BoxCollider2D>().isTrigger = true;
        anim=GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")&&!once)
        {
            if(anim)anim.SetTrigger("moved");

            if (anim2) { anim2.SetTrigger(animationName);  }
            once = true;
        }
        once = false;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag == "Player") && !once)
        {
            if(anim)anim.SetTrigger("moved");
            if (anim2) anim2.SetTrigger(animationName);
            once = true;
        }
        once = false;
    }
}
