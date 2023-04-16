using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
//using EZCameraShake;

public class PlayerControl : MonoBehaviour
{
    bool onceGrounded = false;
    public bool isAudioPlaying;
    [SerializeField] float xSpeedLimiter=9f;
    public GameObject player;
    public float speed;
    private float moveInputHorrizontal;
    private float moveInputVertical;
    public float jumpForce;

    //minor Detail
    private Vector3 velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private bool inAir;
    [Range(0, 3f)] [SerializeField] private float fallingGravity;
    private float startGravity;
   // private bool isdoubleJump = false;
    private bool jumpRequest = false;
    [SerializeField] private float maxJumpForce;
    private float moveV;
    private float xspeed;
    private float yspeed;
    private bool facingRight;


    private Rigidbody2D rb;
    [SerializeField]
    public bool isGrounded;
    public Transform groundCheck;
    public float checkX, checkY;
    public LayerMask whatIsGround;
    private Collider2D groundObjectCollider;
    private int extraJump;
    public int extrajumpinput;

    private int extraDash;
    [SerializeField]private int extraDashInput=1;

    public bool moving;
    public Animator animP;
    public bool isDrop;

    [Header("Sounds")]
    [SerializeField]
    private AudioSource source;
    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip dropSound;
    [Header("extraEffects")]
    //trail
    public Transform eyes;
    public GameObject jumpspark;
    [SerializeField]
    private GameObject trailJump;
    [SerializeField]
    private boxtrail trailscript;

    //check
    public bool isSparkEnable = false;
    public bool isTrailEnable = false;
    [SerializeField] private float extraForce = 0.42f;

    //Dash
    [SerializeField] private float dashForce;
    [SerializeField] private float startDashTime;
    private float dashTime;
    private bool isDashing = false;
    private bool dashRequest;
    [SerializeField]private float restTimeForDashInput;
    private float restTimeForDash;

    //icing
    bool isIcing;
    [SerializeField]private TimeSlower timeManager;
    public CameraShakeNoise camShake;
   [SerializeField] private GameObject dropEffect;
    [SerializeField] private PauseEffect _freezer;
    [Header("Android Controls")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private LeftJoystick left_Joystick;
    private Vector3 leftJoystickInput;
    public bool isAndroidControls ;

    //cinemachine
    public Animator cineMachineAnimator;
    public bool isVoiceGap = false;
    private bool doubleJumpRequest = false;
    [Range(0f,8f)]
    public float OpeningDuration;
    bool isOpening;
    [SerializeField] private float NegativeYspeedLimit = -2f;
    // Use this for initialization
    void Start()
    {
        facingRight = false;
        moving = true;
        rb = player.GetComponent<Rigidbody2D>();
        extraJump = extrajumpinput;
        extraDash = extraDashInput;
        startGravity = rb.gravityScale;
       // trailscript = trailJump.GetComponent<boxtrail>();
    //    trailscript.enabled = false;
        dashTime = startDashTime;
        restTimeForDash = restTimeForDashInput;
        rb.gravityScale = 0f;
        isOpening = true;
        Invoke("activateGravity", OpeningDuration);
        
        
    }
    public void activateDoubleJump()
    {
        extrajumpinput = 1;
        extraJump = 1;
    }
    public void slowSpeedBy(float x)
    {
        speed = speed - x;
    }
    public void disabeDoubleJump()
    {
        extrajumpinput = 0;
        extraJump = 1;
    }
    public void Dojump()
    {
        if (isGrounded && !isDashing)
        {
            jumpRequest = true;
        }
            animP.SetTrigger("jump");
        if (inAir && !isDashing && onceGrounded)
        {
            doubleJumpRequest = true;
        }

    }
    public void activateGravity()
    {
        rb.gravityScale = 2;
        isOpening = false;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.V)){
            isAndroidControls = false;
        }
        if (source.isPlaying)
            isAudioPlaying = true;
        else isAudioPlaying = false;

        //AirControls
        inAir = !isGrounded;
        if (!isOpening)
        {
            if (inAir)
            {
                //Falling
                if (yspeed < 0.5f)
                {
                    rb.gravityScale = startGravity + fallingGravity;
                }
                else
                {
                    rb.gravityScale = startGravity;
                }

            }
            else
            {
                rb.gravityScale = startGravity;
            }
        }
        //Dropping Effects
        DroppingEffects();


        //Is is moving
        if (rb.velocity.x != 0 || yspeed != 0)
        {
            moving = true;
        }
        else
            moving = false;

       
        //IS THE OBJECT GROUNDED
        groundObjectCollider = Physics2D.OverlapBox(groundCheck.position, new Vector2(checkX, checkY), 0, whatIsGround);
        if (groundObjectCollider)
        {
            isGrounded = true;
            onceGrounded = true;
            if (groundObjectCollider.gameObject.layer == 8) isIcing = true;
            else isIcing = false;
        }
        else { isIcing = false; isGrounded = false; }
        // Debug.Log(isIcing);

        
        
        //Left Joystick

        leftJoystickInput = left_Joystick.GetInputDirection();

        float xMovementLeftJoystick = leftJoystickInput.x;
       // Debug.Log(xMovementLeftJoystick);
        float zMovementLeftJoystick = leftJoystickInput.y;

        //Left Right movement
        if (isAndroidControls) 
        {
            moveInputHorrizontal = xMovementLeftJoystick;
           // moveInputHorrizontal = joystick.Horizontal;
        }
        else moveInputHorrizontal = Input.GetAxisRaw("Horizontal");


        // moveV = moveInputHorrizontal * speed * Time.deltaTime;
        if (moveInputHorrizontal >= 0.3f)
        {
            moveV = 1.2f*speed * Time.deltaTime;
        }
        else if (moveInputHorrizontal <= -0.3f)
        {
            moveV = -1.2f * speed * Time.deltaTime;
        }
        else
        {
            moveV = 0f * speed * Time.deltaTime;
        }
        //Facing Right
        if (moveInputHorrizontal > 0.1f)
        {
            if (!facingRight)
            {
                if(eyes)
                    eyes.localPosition = new Vector3(0.59f, 0.37f, 0);
                Flip();
            }
        }
        if(moveInputHorrizontal<-0.1f)
        {
            if (facingRight)
            {
                if(eyes)
                    eyes.localPosition = new Vector3(-0.63f, 0.37f, 0);
                Flip();
            }
          
        }

        if(isAndroidControls)moveInputVertical = joystick.Vertical;
        else moveInputVertical = Input.GetAxis("Vertical");
        //Jump
        //NormalJump
         if (((Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W)) ))
         {
            if (isGrounded && !isDashing)
            {
                jumpRequest = true; 
            }
             if(inAir && !isDashing && onceGrounded)
            {
                doubleJumpRequest = true;
            }
             animP.SetTrigger("jump");
             //Debug.Log("Jump");

         }


        //DashRequest


        if (restTimeForDash <= 0)
        {
            if (moveInputVertical < -0.4f && jumpRequest && extraDash > 0&&false)
            {

                restTimeForDash = restTimeForDashInput;
                animP.SetTrigger("dash");
              //  trailscript.enabled = true;
                camShake.Shake(0.2f, 1.4f);   
                dashRequest = true;
                source.clip = dashSound;
                source.Play();
                //Debug.Log("Dash");

            }
            
        }
        else
        {
            restTimeForDash -= Time.deltaTime;
        }
    
  
       
    }


    //Dealing with physics
    void FixedUpdate()
    {
        xspeed = rb.velocity.x;
        yspeed = rb.velocity.y;

//Limit xSpeed
        if (xspeed > xSpeedLimiter)
            rb.velocity = new Vector2(xSpeedLimiter,rb.velocity.y);
        else if(xspeed< -xSpeedLimiter)
            rb.velocity = new Vector2(-xSpeedLimiter, rb.velocity.y);

        //Limit y speed
        if (yspeed > maxJumpForce)
            rb.velocity = new Vector2(rb.velocity.x, maxJumpForce);
        else if(yspeed<NegativeYspeedLimit*maxJumpForce)
            rb.velocity = new Vector2(rb.velocity.x, -2*maxJumpForce);
        //LeftRight Movement

        Vector3 targetVelocity = new Vector2(moveV * 10f, rb.velocity.y);
       
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, m_MovementSmoothing);
        
        //NormalJump
        if (jumpRequest)
        {

            if (!source.isPlaying && isVoiceGap)
            { source.clip = jumpSound; source.Play(); }
            else { source.clip = jumpSound; source.Play(); }

            if (yspeed < 0.1f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce * 1.1f, ForceMode2D.Impulse);
            }else
                rb.AddForce( Vector2.up * jumpForce,ForceMode2D.Impulse);
            jumpRequest = false;
            doubleJumpRequest = false;
        }

        //Extra Jump
        if (doubleJumpRequest && extraJump > 0 && inAir && !isDashing)
        {
            // Debug.Log("QZooom");
            cineMachineAnimator.SetTrigger("isQzoom");
            if (!source.isPlaying && isVoiceGap)
            { source.clip = jumpSound; source.Play(); }
            else{
                source.clip = jumpSound; source.Play(); }
            //Debug.Log(yspeed);
            // if (isTrailEnable)
            //  trailscript.enabled = true;
            //   isdoubleJump = true;
            if (isSparkEnable)
            {
                GameObject jumpSparkT = Instantiate(jumpspark, player.transform.position, Quaternion.identity);
                Destroy(jumpSparkT, 1f);
            }

            
            if (yspeed > maxJumpForce)
            {
                rb.AddForce(Vector2.up * jumpForce * extraForce, ForceMode2D.Impulse);
            }
            else
            {
                if (yspeed < 0.4f && yspeed > -0.3f && inAir)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.AddForce(Vector2.up * jumpForce * 1.3f, ForceMode2D.Impulse);
                }
                if (yspeed > 0.4f)
                {
                    rb.AddForce(Vector2.up * jumpForce * 1.3f, ForceMode2D.Impulse);

                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.AddForce(Vector2.up * jumpForce * 1.3f, ForceMode2D.Impulse);
                }

            }
                extraJump--;
            doubleJumpRequest = false;
        }
        //DashRequest
        if (dashRequest)
        {
            if (dashTime <= 0)
            {
                rb.velocity = new Vector2(0,Mathf.Min(yspeed,12f));
                dashTime = startDashTime;
                isDashing = false;
                //trailscript.enabled = false;
                dashRequest = false;
                extraDash--;
            }
            else
            {
                
                
                dashTime -= Time.fixedDeltaTime ;
                isDashing = true;
                if(facingRight)
                    rb.velocity = Vector2.right * dashForce;
                else
                    rb.velocity = Vector2.left * dashForce;
                
                //timeManager.DoSlowMotion();
                

            }
        }
    }

  

   void DroppingEffects()
    {
        
        if (isGrounded)
        {
           // if(!isIcing)
                extraJump = extrajumpinput;
            if(restTimeForDash<=0)
                extraDash = extraDashInput;
          //  isdoubleJump = false;
            //trailscript.enabled = false;

            if (isDrop)
            {
                doubleJumpRequest = false;
                if (!isIcing)
                {

                    if (!source.isPlaying&&isVoiceGap) { source.clip = dropSound; source.Play(); }
                    else { source.clip = dropSound; source.Play(); }
                }
                GameObject Gparticle = Instantiate(dropEffect,player.transform.position-new Vector3(0,0.5f,0),Quaternion.identity);
                Destroy(Gparticle, 1f);
    
                if (yspeed == 0 && isIcing)
                {
                    camShake.Shake(0.1f, 0.4f);
     
                }
                else
                { 
                    camShake.Shake(0.1f, 5f);
             
                } 
               // CameraShaker.Instance.ShakeOnce(shakeImpulse, 7f, 0.2f, 0.4f);
                if (yspeed < -6f)
                {
                    animP.SetTrigger("hardDrop");
                }
                else
                    animP.SetTrigger("drop");
                isDrop = false;
            }
        }
        else
            isDrop = true;
    }

    void Flip()
    {

        facingRight = !facingRight;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(checkX, checkY, 1));
    }
   
}
