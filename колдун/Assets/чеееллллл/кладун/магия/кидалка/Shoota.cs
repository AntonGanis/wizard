using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoota : MonoBehaviour
{
    Animator animator;
    public int TIP;//0=1.1.1  1=1.1.2  2=1.1.3  3=3.1.2  4=4.1.1  5=4.1.3
    public float[] LimitTime;
    public float[] LimitTimeShoot;
    public float time;
    float timeShoot;
    
    public GameObject[] bullet;
    public Transform spawn;
    CreatePoint Create;

    bool combo;
    Wall www;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        Create = transform.parent.transform.parent.GetComponent<CreatePoint>();
    }
    void Update()
    {
        if (time > 0 && Create.isLocked == true)
        {
            timeShoot += Time.deltaTime;
            if (Input.GetMouseButtonUp(0) && timeShoot > LimitTimeShoot[TIP])
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 2f))
                {
                    Wall trtComponent = hit.collider.GetComponent<Wall>();
                    if (trtComponent != null)
                    {
                        for (int i = 0; i < trtComponent.TIP.Length; i++)
                        {
                            if (trtComponent.TIP[i] == TIP)
                            {
                                www = trtComponent;
                                combo = true;
                                break;
                            }
                        }
                    }
                }
                if (TIP == 0)
                {
                    animator.SetInteger("Do", 3);
                }
                else if (TIP == 1)
                {
                    animator.SetInteger("Do", 7);
                }
                else if (TIP == 5)
                {
                    animator.SetInteger("Do", 4);
                }
                timeShoot = 0;
            }
            
            time -= Time.deltaTime;
        }
    }
    public void Shoot()
    {
        if(combo == false)
        {
            Instantiate(bullet[TIP], spawn.position, spawn.rotation);
        }
        else
        {
            Vector3 forceDirection = (www.transform.position - transform.position).normalized;
            float forceMagnitude = 10f; 
            www.Fly(forceDirection * forceMagnitude);
            combo = false;
        }
    }
}