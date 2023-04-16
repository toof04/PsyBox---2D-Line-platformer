using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicDrop : MonoBehaviour
{
    [SerializeField]private AudioSource backgroundSource;
    public AudioClip dropMusic;
    bool once = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!once&&collision.CompareTag("Player"))
        if (backgroundSource!=null)
        {
            { backgroundSource.clip = dropMusic; backgroundSource.Play(); }

                once = true;    
        }
    }
}
