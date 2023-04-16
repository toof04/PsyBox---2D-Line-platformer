using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookDownCamera : MonoBehaviour
{
    [SerializeField] Animator camAnim;
    // Start is called before the first frame update
    void Start()
    {
        if (camAnim) camAnim.SetTrigger("Entry");
        if(camAnim)camAnim.SetTrigger("isOutcast");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
