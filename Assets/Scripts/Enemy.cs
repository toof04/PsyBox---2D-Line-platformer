using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using EZCameraShake;
public class Enemy : MonoBehaviour {
    public int health;
    public GameObject burst;
    [SerializeField]
    private AudioSource source;
    public AudioClip pickup;
    public AudioClip thud;
    private float myPitch;
    // Update is called once per frame
    void Awake()
    {
        myPitch = source.pitch;
    }
    void Update () {
        if (health <= 0)
        {
            source.clip = pickup;
            source.pitch -= 0.02f;
            source.Play();
            source.pitch = myPitch;
            Destroy(gameObject, 0.1f);
        }
       

    }
    public void TakeDamage(int damage)
    {
        source.clip = pickup;
        source.Play();
        source.pitch += 0.02f;
        health = health - damage;
        GameObject blood = Instantiate(burst,transform.position,Quaternion.identity);
        Destroy(blood, 2f);
        Debug.Log("aah");
      //  CameraShaker.Instance.ShakeOnce(2.5f, 2.5f, 0.1f, 1f);
      ///////////////////////////////
    }
}
