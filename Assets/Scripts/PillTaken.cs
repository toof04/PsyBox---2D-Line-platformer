using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillTaken : MonoBehaviour
{

    [SerializeField] private TimeSlower _timeSlower;

    [SerializeField] private LineCreater _lineCreator;
    [SerializeField] private Animator cineMachineAnimator;
    private Animator animP;
    [SerializeField]
    private AudioSource source;
    public AudioClip omSound;
    [SerializeField] private float slowTime = 2f;
    [SerializeField] private float pillTime=5f;
    bool onceTaken = false;
    // Start is called before the first frame update
    private void Start()
    {
        animP = GetComponent<Animator>();
        onceTaken = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakePill();
    }
    private void TakePill()
    {
        if (!onceTaken)
        {

            if (cineMachineAnimator) cineMachineAnimator.SetTrigger("isRotateCameraEffect");
            if (_lineCreator) _lineCreator.StartAutoIcing(pillTime);
            if (animP) animP.SetTrigger("pillTaken");
            //if (!source.isPlaying)
            {
                source.clip = omSound;
                source.Play();
            }
            if (_timeSlower) _timeSlower.DoSlowmotion(slowTime);
            //onceTaken = true;
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
