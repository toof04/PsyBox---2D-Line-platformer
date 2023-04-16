using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateDoubleJump : MonoBehaviour
{
    [SerializeField] private PlayerControl pc;
    private Animator anim;
    public GameObject light;
    public GameObject jumpText;
    bool once = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        once = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Invoke("activate_tutorial", 0.1f);
        if (!once) {
            activate_tutorial();
            pc.activateDoubleJump();
            anim.SetTrigger("dJump");
            light.SetActive(true);
            jumpText.SetActive(true);
            Destroy(gameObject, 1f);
            once = true; 
        } 
    }
    private void activate_tutorial()
    {
        GameControlScript.isTutorialTime = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
