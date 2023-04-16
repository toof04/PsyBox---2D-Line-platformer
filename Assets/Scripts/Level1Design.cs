using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level1Design : MonoBehaviour {
    [SerializeField] private GameObject DestParticles;
    [SerializeField] private Vector3[] Nextpositions;
    [SerializeField]private float checkX, checkY;
    public LayerMask whatIsPlayer;
    private bool isPlayer;
    [SerializeField]private int x = 1;
    private bool s,t;
    private ParticleSystem ps;
    private GameObject tempParticle;
    [SerializeField] private GameObject playerControl;
    [SerializeField] private TextMeshProUGUI helptxt;
    [SerializeField] GameControlScript GM;
    [SerializeField]private float extraTimeInfo=2f;
    [SerializeField] private float extraTimeForEnd = 3f;
    [SerializeField]private AudioSource source;
    public AudioClip endSound;
    private bool isEndSound=true;
    private bool isEndSound2 = true;
    //  private Text ;
    // Use this for initialization
    void Start () {
        isPlayer = false;
        x = 1;
        s = true;t = true;
       // helptxt = gameObject.GetComponent<TextMeshProUGUI>();
        if (Nextpositions.Length > 0)
        {
            tempParticle = Instantiate(DestParticles, Nextpositions[0], Quaternion.identity);
            helptxt.SetText("Hello Human, Press Space to jump");
            SetHelpTxt();
        }
        isEndSound = true;
    }
	
	// Update is called once per frame
	void Update () {

        
        isPlayer = Physics2D.OverlapBox(Nextpositions[x-1], new Vector2(checkX, checkY), 0, whatIsPlayer);        
        if (isPlayer && s && x<Nextpositions.Length)
        {
            SetHelpTxt();
            //SetHelpTxt();
            
            if (tempParticle)
            {
                ps = tempParticle.GetComponent<ParticleSystem>();
                var main = ps.main;
                main.loop = false;
                Destroy(tempParticle, 2f);
            }
            tempParticle = Instantiate(DestParticles, Nextpositions[x], Quaternion.identity);
            x++;
           s = false;
        }
       else
       {
           s = true;
       }
        //END Game
        if (x == Nextpositions.Length)
        {
            if (extraTimeInfo < 0)
            {
                helptxt.SetText("");
            }
            else { extraTimeInfo -= Time.deltaTime; }

            isPlayer = Physics2D.OverlapBox(Nextpositions[x-1], new Vector2(checkX, checkY), 0, whatIsPlayer);
            if (isEndSound2)
            {
                source.clip = endSound;
                isEndSound2 = false;

            }
            if (isPlayer)
            {
                if (extraTimeForEnd< 0)
                {  
                    Debug.Log("end");
                    GM.BackGame();
                }
                else { extraTimeForEnd -= Time.deltaTime;
                    helptxt.SetText("WohooooO !!!!");
                    if (isEndSound)
                    {
                        //Debug.Log("endSound");
                        
                        source.Play();
                        isEndSound = false;
                    }
                }               
            }
        }      
    }
    void SetHelpTxt()
    {
        switch (x-1) {
            case 0:
        {
            helptxt.SetText("Hello Human, Press Space to jump");
        } break;
            case 1:
        {
            helptxt.SetText("Tap space in air to double jump");
        }
                break;
            case 2:
        {
            helptxt.SetText("Hold down key and press space to dash on objects");
        }
                break;
            case 3:
        {
            helptxt.SetText("Yes Human! This cube has magical powers...\n hold mouse1 to check");
        }
                break;
            case 4:
        {
            helptxt.SetText("Level Complete");
        } break;
            default:
        {
                    helptxt.SetText("Hoomans shud be cannibals");
        }
                break;
    }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if(Nextpositions.Length>0)
            Gizmos.DrawWireCube(Nextpositions[0], new Vector3(checkX, checkY, 1));
    }
}
