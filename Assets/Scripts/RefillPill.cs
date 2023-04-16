using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillPill : MonoBehaviour
{
    [SerializeField] private TimeSlower _timeSlower;

   // [SerializeField] private LineCreater _lineCreator;
    [SerializeField] private Animator cineMachineAnimator;
    private Animator animP;
    [SerializeField]
    private AudioSource source;
    public AudioClip omSound;
    [SerializeField] private float slowTime = 2f;
    [SerializeField] private float pillTime = 5f;
    private bool once=false;
    [SerializeField] private bool isRefillable = false;
    // Start is called before the first frame update
    void Start()
    {
        animP = GetComponent<Animator>();
        once = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!once&&collision.CompareTag("Player"))
        { TakePill(); 
            if(!isRefillable)once = true; }
    }
   
    private void TakePill()
    {

        if (cineMachineAnimator) cineMachineAnimator.SetTrigger("isQzoom");
        //  if (_lineCreator) {if(_lineCreator.MaxFluidslimit>_lineCreator.Fluidslimit) _lineCreator.Fluidslimit += pillTime; }
        LineCreater.Fluidslimit += pillTime;
        if (animP) animP.SetTrigger("pillTaken");
        if (isRefillable) animP.SetTrigger("isRefill");
        if (source)
        {
            source.clip = omSound;
            source.Play();
        }
        if (_timeSlower) _timeSlower.DoSlowmotion(slowTime);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
