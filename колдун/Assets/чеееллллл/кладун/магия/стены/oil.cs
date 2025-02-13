using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oil : MonoBehaviour
{
    public float xz;
    public float y;
    Vector3 size;

    void Update()
    {
        size = transform.localScale;

        size.x = size.x + xz;
        size.z = size.z + xz;
        if (y != 0)
        {
            size.y = size.y + y;
        }
        transform.localScale = size;
    }
}
