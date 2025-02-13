using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int I;
    public int[] TIP;

    void Start()
    {
        if (I == -1) { I = Random.Range(1, 100); }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Wall>() && col.gameObject.GetComponent<Wall>().I > I)
        {
            Destroy(transform.parent.gameObject);
        }
    }
    public void Fly(Vector3 targetVelocity)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        if (gameObject.GetComponent<Animator>()) gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<Speed>().enabled = true;
        float mass = rb.mass;
        Vector3 calculatedForce = mass * targetVelocity * mass * 0.2f; 
        rb.AddForce(calculatedForce, ForceMode.Impulse);
    }


}
