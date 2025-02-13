using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeWall : MonoBehaviour
{
    Animator animator;
    public int TIP;//0=1.3.1  1=1.3.2  2=1.3.3  3=2.3.1  4=2.3.2  5=3.3.1  6=3.3.2  7=4.3.1  8=4.3.2  9=4.3.3  
    public float[] LimitTime;
    public float[] LimitTimeShoot;
    public float time;
    float timeShoot;
    public Trigger[] trigi;
    public GameObject[] AYE;
    CreatePoint Create;

    int I;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        Create = transform.parent.transform.parent.GetComponent<CreatePoint>();
    }

    void Update()
    {
        if (time > 0 )
        {
            timeShoot += Time.deltaTime;
            if (trigi[TIP].mogno == true && Input.GetMouseButtonDown(1) && timeShoot > LimitTimeShoot[TIP])
            {
                if (TIP == 0 || TIP == 1 || TIP == 2 || TIP == 3 || TIP == 5)
                {
                    animator.SetInteger("Do", 1);
                }
                else if (TIP == 7)
                {
                    animator.SetInteger("Do", 2);
                }
                timeShoot = 0;
            }
            

            time -= Time.deltaTime;
        }
    }
    public void Make()
    {
        GameObject U = Instantiate(AYE[TIP], trigi[TIP].transform.position, trigi[TIP].transform.rotation);
        if(U.transform.GetChild(0).GetComponent<Wall>()) U.transform.GetChild(0).GetComponent<Wall>().I = I;
        I++;
    }
}
