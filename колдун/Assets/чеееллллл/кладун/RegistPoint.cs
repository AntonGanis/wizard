using UnityEngine;
using System.Collections.Generic;

public class RegistPoint : MonoBehaviour
{
    public bool yes;
    private void OnTriggerEnter2D(Collider2D other)
    {
        DrawPoint poin = other.GetComponent<DrawPoint>();
        if (poin != null && poin.end != true && yes == false)
        {
            yes = true;
        }
    }
}