using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class takeDrug : MonoBehaviour
{
    [SerializeField] private TimeSlower _timeSlower;
    public PostProcessVolume pp;
    private ColorGrading CG;
    // [SerializeField] private LineCreater _lineCreator;
    [SerializeField] private Animator cineMachineAnimator;
    [SerializeField] private Animator panelAnimator;
    private Animator animP;
    [SerializeField]
    private AudioSource source;
    public AudioClip omSound;
    [SerializeField] private float slowTime = 2f;
    [SerializeField] private float pillTime = 5f;
    private bool once = false;
    public ArrayList decreaseColorNumber = new ArrayList();
   
    [Header("player Stuff")]
    [SerializeField] private GameObject lineButton;
    public CameraShakeNoise camShake;
    [SerializeField] private PlayerControl PC;
    [SerializeField] private bool slowSpeed=false;
    [SerializeField] private GameObject jumpButton;
    [SerializeField] private GameObject blackRain;
    
    // Start is called before the first frame update
    void Start()
    {
        animP = GetComponent<Animator>();
        once = false;
        pp.profile.TryGetSettings(out CG);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!once && collision.CompareTag("Player"))
        {
            TakePill();
            once = true;
        }
    }

    private void TakePill()
    {
        Invoke("ColorDecrease", 3f);
        if (cineMachineAnimator) cineMachineAnimator.SetTrigger("trippy");
        if (panelAnimator) panelAnimator.SetTrigger("trippy");

        //  if (_lineCreator) {if(_lineCreator.MaxFluidslimit>_lineCreator.Fluidslimit) _lineCreator.Fluidslimit += pillTime; }
        LineCreater.Fluidslimit += pillTime;
        if(camShake)camShake.Shake(1f, 5f);
        if (animP) animP.SetTrigger("trippy");
        if (source)
        {
            source.clip = omSound;
            source.Play();
        }
        if (_timeSlower) _timeSlower.DoSlowmotion(slowTime);
    }
    private void ColorDecrease()
    {
        CG.saturation.value = CG.saturation.value - 40f;
        if (lineButton) lineButton.SetActive(false);
        if (jumpButton) jumpButton.SetActive(false);
        if (PC) PC.disabeDoubleJump();
        if (PC && slowSpeed) PC.slowSpeedBy(5f);
        if (blackRain) blackRain.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
