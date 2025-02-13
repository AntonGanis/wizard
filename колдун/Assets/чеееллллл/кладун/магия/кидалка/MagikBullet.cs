using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagikBullet : MonoBehaviour
{
    public int Speed;
    Vector3 lastPos;
    public bool ship;
    public int push;
    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        RaycastHit hit;
        Debug.DrawLine(lastPos, transform.position);
        if (Physics.Linecast(lastPos, transform.position, out hit) && hit.transform != gameObject.transform)
        {
            if (ship == false)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.transform.parent = hit.transform;
                gameObject.GetComponent<ShipKostil>().enabled = true;
                if (push != 0)
                {
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 pushDirection = hit.transform.position - transform.position;
                        pushDirection.Normalize();
                        rb.AddForce(pushDirection * push, ForceMode.Impulse);
                    }
                }
                Destroy(gameObject.GetComponent<MagikBullet>());
            }
        }
        lastPos = transform.position;

    }
}