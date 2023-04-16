using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCam : MonoBehaviour
{
    // Start is called before the first frame 
    public GameObject extra1;
    public GameObject extra2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (extra1) extra1.SetActive(true);
            if (extra2) extra2.SetActive(true);
        }
    }

}
