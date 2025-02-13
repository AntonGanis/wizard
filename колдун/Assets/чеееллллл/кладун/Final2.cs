using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final2 : MonoBehaviour
{
    public Animator animator;
    public int element;
    public int ukazatel;
    public int stepen;
    public bool END;

    public List<GameObject> Objects;

    GameObject plar;
    GameObject arm;
    int o;
    void Start()
    {
        plar = FindObjectOfType<Move>().gameObject;
        arm = FindObjectOfType<MakeWall>().gameObject;
    }
    void redactWall(int i)
    {
        arm.GetComponent<MakeWall>().TIP = i;
        arm.GetComponent<MakeWall>().time = arm.GetComponent<MakeWall>().LimitTime[i];
    }
    void redactShoota(int i)
    {
        arm.GetComponent<Shoota>().TIP = i;
        arm.GetComponent<Shoota>().time = arm.GetComponent<Shoota>().LimitTime[i];
        arm.GetComponent<Strua>().time = 0;
    }
    void redactStrua(int i)
    {
        arm.GetComponent<Strua>().TIP = i;
        arm.GetComponent<Strua>().time = arm.GetComponent<Strua>().LimitTime[i];
        arm.GetComponent<Shoota>().time = 0;
    }
    void Reload()
    {
        for (int i = 0; Objects.Count > i; i++)
        {
            Destroy(Objects[i]);
        }
        stepen = 0;
        END = true;
    }
    void Update()
    {
        if (o > 20) {
            for (int i = 0;Objects.Count > i; i++)
            {
                if (Objects[i] == null)
                {
                    Objects.RemoveAt(i);
                }
            }
            if (!Input.GetMouseButton(0) && element != 0 && ukazatel != 0 && stepen != 0)
            {
                if(element == 1)
                {
                    if (ukazatel == 1)
                    {
                        if (stepen == 1)
                        {
                            redactShoota(0);
                            Reload();
                        }
                        else if (stepen == 2)
                        {
                            redactShoota(1);
                            Reload();
                        }
                    }
                    else if (ukazatel == 2)
                    {
                        if (stepen == 1)
                        {
                            plar.GetComponent<Health>().DopHealth(50, 0.9f, 5);
                            animator.SetTrigger("броня1");
                            Reload();
                        }
                        else if (stepen == 2)
                        {
                            plar.GetComponent<Health>().DopHealth(150, 0.7f, 10);
                            animator.SetTrigger("броня1");
                            Reload();
                        }
                        else if (stepen == 3)
                        {
                            plar.GetComponent<Health>().DopHealth(200, 0.5f, 20);
                            animator.SetTrigger("броня1");
                            Reload();
                        }
                    }
                    else if (ukazatel == 3)
                    {
                        if (stepen == 1)
                        {
                            redactWall(0);
                            Reload();
                        }
                        else if (stepen == 2)
                        {
                            redactWall(1);
                            Reload();
                        }
                        else if (stepen == 3)
                        {
                            redactWall(2);
                            Reload();
                        }
                    }
                }
                else if (element == 2)
                {
                    if (ukazatel == 1)
                    {
                        if (stepen == 1)
                        {
                            redactStrua(0);
                            Reload();

                        }
                    }
                    else if (ukazatel == 2)
                    {
                        if (stepen == 1)
                        {
                            plar.GetComponent<BreatheInWater>().Done(-1);
                            plar.GetComponent<BreatheInWater>().time+=90;
                            animator.SetTrigger("дышать");
                            Reload();
                        }
                    }
                    else if (ukazatel == 3)
                    {
                        if (stepen == 1)
                        {
                            redactWall(3);
                            Reload();
                        }
                    }
                }
                else if (element == 3)
                {
                    if (ukazatel == 1)
                    {
                        if (stepen == 1)
                        {
                            redactStrua(1);
                            Reload();
                        }
                    }
                    else if (ukazatel == 2)
                    {
                        plar.GetComponent<Aura>().Go(200, 3, DamageType.Fire, 0);
                        animator.SetTrigger("аура");
                        Reload();
                    }
                    else if (ukazatel == 3)
                    {
                        if (stepen == 1)
                        {
                            redactWall(5);
                            Reload();
                        }
                    }
                }
                else if (element == 4)
                {
                    if(ukazatel == 1)
                    {
                        if (stepen == 1)
                        {
                            redactShoota(5);
                            Reload();
                        }
                    }
                    else if(ukazatel == 2)
                    {
                        if (stepen == 1)
                        {
                            plar.GetComponent<UpSpeed>().AddSpeed(2, 150);
                            animator.SetTrigger("скорость");
                            Reload();
                        }
                    }
                    else if (ukazatel == 3)
                    {
                        if (stepen == 1)
                        {
                            redactWall(7);
                            Reload();
                        }
                    }
                }
            }
            o = 0;
        }
        else
        {
            o++;
        }
    }
}
