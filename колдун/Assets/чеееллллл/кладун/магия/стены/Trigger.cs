using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool mogno;
    public string Tag;
    public Transform[] objectsToCheck;
    int o;
    void Update()
    {
        if(o > 20)
        {
            int g = 0;
            for(int i = 0; i < objectsToCheck.Length; i++)
            {
                Collider[] colliders = Physics.OverlapSphere(objectsToCheck[i].position, 0.1f);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag(Tag))
                    {
                        g++;
                        break;
                    }
                }
            }
            if (g == objectsToCheck.Length)
            {
                mogno = true;
            }
            else
            {
                mogno = false;
            }
            o = 0;
        }
        else { o++; }
    }
}
