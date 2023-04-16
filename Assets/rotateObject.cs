using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
    [SerializeField]private float speed=50f;
    public bool isRotating = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {   
        if(isRotating)
            transform.Rotate(0, 0, speed * Time.deltaTime); //rotates 50 degrees per second around z axis
    }
}
