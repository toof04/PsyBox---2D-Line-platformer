using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Vector2 ReachedPoint;
    public static float fluids;
    Animator anim;
     bool entered=false;
    private void Start()
    {
        ReachedPoint = Vector2.zero;
        fluids = 0;
        anim = GetComponent<Animator>();
        entered = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!entered)
            {
                anim.SetTrigger("checkPoint");
                ReachedPoint = transform.position;
                fluids = LineCreater.Fluidslimit;
                entered = true;
            }
        }
    }
}
