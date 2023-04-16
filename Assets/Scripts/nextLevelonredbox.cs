using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class nextLevelonredbox : MonoBehaviour
{

    public Animator transitoinAnim;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadScene());
        }
        
            
            
    }
    IEnumerator LoadScene()
    {
        if(transitoinAnim)transitoinAnim.SetTrigger("end");
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneName);
    }
    void Update()
    {
        
    }
}
