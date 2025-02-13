using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glif : MonoBehaviour
{
    public int don;
    public int start;

    public int element;
    public int ukazatel;
    public bool stepen;
    void Start()
    {
        start = transform.childCount;
        if (start < don)
        {
            Destroy(gameObject);
        }
        else
        {
            Final2 fin = FindObjectOfType<Final2>();
            if (element != 0) { fin.element = element; }
            if (ukazatel != 0) { fin.ukazatel = ukazatel; }
            if (stepen) { fin.stepen++; }
            fin.Objects.Add(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(2) || Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }
    }
}
