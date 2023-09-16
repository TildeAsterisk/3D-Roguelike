using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float speed = 10.0f;

    void Update()
    {
        transform.position = target.position + (transform.position - target.position).normalized * distance;
        transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
    }
}

