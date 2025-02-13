using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rezist : MonoBehaviour
{
    Animator animator;
    public int TIP;//0=1.2.1  1=1.2.2  2=1.2.3  3=2.2.1  4=3.2.2
    public float[] LimitTime;
    public float time;

    bool _new;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (time > 0)
        {
            if (TIP == 0)
            {
                animator.SetInteger("Do", 7);
            }
            else if (TIP == 3)
            {
                animator.SetInteger("Do", 8);
            }
            time -= Time.deltaTime;
        }
    }
}
