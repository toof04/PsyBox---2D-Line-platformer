using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class DistanceLens : MonoBehaviour
{
    public Vector2 d1;
    public Vector2 d2;
    public float limiter=-87f;
    public Transform playerpos;
    public PostProcessVolume pp;
    private LensDistortion ld;
    private float d;
    float inc;
    float x1, x2;
    [SerializeField]float multiplier;
    //this one for fast transitions
    public float x3=0f;
    bool t3 = true;
    // Start is called before the first frame update
    void Start()
    {
        pp.profile.TryGetSettings(out ld);
        ld.intensity.value = 0;
        d =d1.x-d2.x;
        inc = limiter / d;
        x1 = playerpos.position.x;
        x2 = playerpos.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        x1 = playerpos.position.x;
        float diff = x1-x2;
        if (x1 > d1.x)
        {
            //Debug.Log(diff);
            if (diff > 1f)
            {
                ld.intensity.value = Mathf.Max(ld.intensity.value - inc, limiter);
                x2 = playerpos.position.x;
            }
            if (diff < (-1f))
            {
                ld.intensity.value = Mathf.Min(ld.intensity.value + inc, 0);
                x2 = playerpos.position.x;
            }
            if (t3 && (x1 > x3) && x3 != 0)
            {
                ld.intensity.value = -65f;
                t3 = false;
            }
        }
       // ld.intensity.value = inc;
       // ld.intensity.value = Mathf.Lerp(ld.intensity.value,-87,.5f*Time.deltaTime);
   
    }
}
