using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlower : MonoBehaviour
{

    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;
    public float t;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        t = Time.timeScale;
        if (!GameControlScript.GameIsPaused && !GameControlScript.isTutorialTime)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.C))
            DoSlowmotion(2f);

    }
    public void DoSlowmotion(float t)
    {
        slowdownLength = t;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

 
}
