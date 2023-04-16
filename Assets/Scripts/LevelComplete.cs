using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class LevelComplete : MonoBehaviour
{
    [Header("Main Settings")]
    public Animator transitoinAnim;
    public string currentScene;
    public string NextsceneName;
    [SerializeField] private Animator cineMachineAnimator;
    //Level Alpha
    [Header("alpha")]
    public float AlphaTimer = 42f;
    float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        t = 0f;
        cineMachineAnimator.SetTrigger("isFirstLevel");
        cineMachineAnimator.SetTrigger("Entry");
        
    }
    
    void Update()
    {
        switch (currentScene)
        {
            case "alpha":
                {
                    if (t > AlphaTimer)
                    {
                        StartCoroutine(LoadScene());
                    }
                    else t = t + Time.deltaTime;
                }
                break;
            
        }

    }
    IEnumerator LoadScene()
    {
       // transitoinAnim.SetTrigger("end");
        if (cineMachineAnimator) cineMachineAnimator.SetTrigger("isSceneEnd");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(NextsceneName);
    }
}
