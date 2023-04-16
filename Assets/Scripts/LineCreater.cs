using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class LineCreater : MonoBehaviour {
    public GameObject chalk;
    public GameObject brush;
    [SerializeField]
    public float MaxFluidslimit;
    [SerializeField]
    private float startFluidsWith=0;
    public static float Fluidslimit;
    public void _fluidsLimitAdd(float extra){  Mathf.Clamp(Fluidslimit, (Fluidslimit + (extra*MaxFluidslimit/100)),MaxFluidslimit); }
    public GameObject linePrefab;
    public GameObject BouncelinePrefab;
    Line activeLine1;
    Line activeLine2;
    Rigidbody2D rb;
    [SerializeField]
    private bool isGrounded;
    public Transform groundCheck;
    public float checkX, checkY;
    public LayerMask whatIsGround;
    private GameObject lineGO;
    private bool isIcing;
    [SerializeField] private float decreaseFluidRate = 2f;
    [SerializeField] private float increaseFluidRate = 1f;
    [SerializeField] private bool isRegeneration=true;
    [Header("Unity Stuff1")]
    public Image fluidsBar;
    private bool released = false;
    private bool pushed= false;
    [SerializeField]private float percentOnceDecrease=25f;
    [SerializeField] private float percentLimitOfRegeneration= 50f;
    public float percentnow=100f;
    [Header("Unity Stuff2")]
    public GameObject lineTopParticle;
    public GameObject lineTop;
    private Animator animP;
    [SerializeField] private Animator animLipJhip;
    bool lipJhip = false;
    [SerializeField] private bool limiter=true;
    // Update is called once per frame
    public void StartAutoIcing(float iceTimer)
    {
        pushed = true;
        Fluidslimit = iceTimer;
    }
    public void StopAutoIcing()
    {
        pushed = false;
        activeLine1 = null;
    }
    public void disablePower()
    {
        isRegeneration = false;
        Fluidslimit = 0;
    }
    public void Start()
    {
        Fluidslimit = startFluidsWith;
        animP = chalk.GetComponent<Animator>();
        //Fluidslimit = MaxFluidslimit;
        pushed = false;
        released = false;
        lipJhip = false;
    }
    void Update()
    {
        if (Fluidslimit >= MaxFluidslimit&&limiter) Fluidslimit = MaxFluidslimit;
        percentnow = (Fluidslimit / MaxFluidslimit * 100);
        fluidsBar.fillAmount = Fluidslimit / MaxFluidslimit;
        
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(checkX, checkY), 0, whatIsGround);
        if (isGrounded)
        {
            activeLine1 = null;
            activeLine2 = null;
        }
        if (!isIcing&&isRegeneration)
        {
            if (percentnow < percentLimitOfRegeneration)
            {
                IncreaseRateOfFluid();
                
            }
            if (percentnow < percentLimitOfRegeneration)
            {
                IncreaseRateOfFluid();

            }
        }
        else
        {
          //  if(animP)animP.SetTrigger("z");
            //if(animLipJhip)animLipJhip.SetTrigger("lipjhip");
            //lipjhip animation
        }
        if (!isGrounded)
        {
            ///Fluid Limit reached
            ///
           // Debug.Log(Fluidslimit);
            if (Fluidslimit >= 0)
            {

                //NormalLine
                if ((Input.GetKeyDown(KeyCode.Space) || pushed))
                {
                    if (lineTopParticle)
                    {
                        GameObject lineparticols = Instantiate(lineTopParticle, brush.transform.position, Quaternion.identity);
                        Destroy(lineparticols, 1f);
                    }
                    float minFluid = ((percentOnceDecrease * MaxFluidslimit) / 100f);
                    if (Fluidslimit >= minFluid)
                    {
                        lineGO = Instantiate(linePrefab);
                        Fluidslimit -= (percentOnceDecrease / 100f * MaxFluidslimit);
                        activeLine1 = lineGO.GetComponent<Line>();
                    }//lineTop

                    if (lineTop && (Fluidslimit >= 0.25f) && activeLine1)
                    {
                        Instantiate(lineTop, brush.transform.position - new Vector3(0, .1f, 0), Quaternion.identity);

                    }
                    else
                    {

                    }
                    
                    isIcing = true;
                    pushed = false;
                }
                if (Input.GetKeyUp(KeyCode.Space)||released)
                {
                    if (lineTop && activeLine1)
                        Instantiate(lineTop, brush.transform.position - new Vector3(0, .1f, 0), Quaternion.identity);
                    if (lineTopParticle)
                    {
                        GameObject lineparticols=Instantiate(lineTopParticle, brush.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        Destroy(lineparticols, 1f);
                    }
                    activeLine1 = null;
                    isIcing = false;
                    released = false;

                }
                if (activeLine1 != null)
                {
                    
                    activeLine1.UpdateLine(brush.transform.position);
                    Fluidslimit -= decreaseFluidRate*Time.deltaTime;
                }

                //BounceLine();
            }
            else
            {

                activeLine1 = null;
                isIcing = false;
            }
        }
        else isIcing = false;
    }
    void IncreaseRateOfFluid()
    {
        Fluidslimit += increaseFluidRate*Time.deltaTime;
        Fluidslimit = Mathf.Clamp(Fluidslimit, 0f, MaxFluidslimit);

    }

    void BounceLine()
    {
        //Bounce
        if (Input.GetMouseButtonDown(1))
        {
            GameObject lineGO = Instantiate(BouncelinePrefab);
            activeLine2 = lineGO.GetComponent<Line>();
        }
        if (Input.GetMouseButtonUp(1))
        {
            activeLine2 = null;
        }
        if (activeLine2 != null)
        {
            activeLine2.UpdateLine(brush.transform.position);
            Fluidslimit -= Time.deltaTime;
        }
    }
    //GizmosForGroundCheck
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(checkX, checkY, 1));
    }
    public void OnPress()
    {
        pushed = true;

    }
    public void OnRelease()
    {
        pushed = false;
        released = true;

    }
}
