using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMove : MonoBehaviour
{
    [SerializeField] float velocity = 2f;

    void Update()
    {
        transform.position += Vector3.left * velocity * Time.deltaTime;
    }
}