using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreLines : MonoBehaviour {
    private bool x;
    // Use this for initialization
    void Start () {
        x = true;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Physics2D.IgnoreLayerCollision(8, 13,x);
           
            x = !x;
        }
        

    }
}
