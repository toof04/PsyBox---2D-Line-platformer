using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateLineButton : MonoBehaviour
{
    [SerializeField] private GameObject lineButton;
    [SerializeField]private Animator anim;
    public GameObject baba;
    [SerializeField] private TimeSlower _timeSlower;
    [SerializeField] private float slowTime = 2f;
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
        if (!once)
        {
            activate_tutorial();
            if (_timeSlower) _timeSlower.DoSlowmotion(slowTime);
            lineButton.SetActive(true);
            if(anim)anim.SetTrigger("dJump");
            baba.SetActive(true);
            Destroy(gameObject, 1.2f);
            once = true;
        }
    }
    private void activate_tutorial()
    {
        GameControlScript.isTutorialTime = true;
    }
    // Update is called once per frame

}
