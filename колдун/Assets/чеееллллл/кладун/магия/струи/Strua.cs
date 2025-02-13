using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strua : MonoBehaviour
{
    Animator animator;
    public int TIP;//0=2.1.1  1=3.1.1
    public float[] LimitTime;
    public float time;
    public float timeShoot;
    public float LimitTimeShoot;
    public float BulletForce;

    public Transform[] bullet;
    public Transform spawn;
    CreatePoint Create;

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
            if (Input.GetMouseButton(0))
            {
                if (timeShoot > LimitTimeShoot)
                {
                    if (TIP == 0)
                    {
                        animator.SetInteger("Do", 5);
                    }
                    else if (TIP == 1)
                    {
                        animator.SetInteger("Do", 6);
                    }
                    Transform BulletInstance = (Transform)Instantiate(bullet[TIP], spawn.position, Quaternion.identity);
                    BulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * BulletForce);
                    timeShoot = 0;
                }
            }
            
            time -= Time.deltaTime;
        }
    }
}
