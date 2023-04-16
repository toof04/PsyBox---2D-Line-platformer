using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimTarget : MonoBehaviour
{
    public Transform target;
    public float offset=0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = target.position - transform.position;
        float rotZ = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

    }
}
