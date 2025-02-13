using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KostilAnimation : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetInteger("Do", 0);
    }
}
