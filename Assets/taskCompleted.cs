using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taskCompleted : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] public AudioClip LeverMusic;
    [SerializeField] private GameObject lever;
    bool once = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pill"))
        {
            collision.GetComponent<Animator>().SetTrigger("completed");
            collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Destroy(collision.gameObject,0.6f);
            if(lever)doSomething();

        }
    }
    // Start is called before the first frame update
    void doSomething()
    {
        lever.GetComponent<Animator>().SetTrigger("completed");
        Destroy(lever, 1f);
        if (!once&&source&&LeverMusic)
            if (source != null)
            {
                { source.clip = LeverMusic; source.Play(); }

                once = true;
            }
    }
}
