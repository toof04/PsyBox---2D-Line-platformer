using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    AudioSource s;
    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControlScript.GameIsPaused)
        {
            s.pitch = 0.5f;
        }
        else
        {
            s.pitch = 1;
        }
    }
}
