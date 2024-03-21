using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Space relativeTo = Space.Self;
    public float speed = 3f;

    void Update()
    {
        transform.Rotate(0, speed, 0, relativeTo);
    }
}
