using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipKostil : MonoBehaviour
{
    int o;
    void Update()
    {
        if(o > 5)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<Damage>());
            Destroy(gameObject.GetComponent<MagikBullet>());
            Destroy(gameObject.GetComponent<ShipKostil>());
        }
        else
        {
            o++;
        }
    }
}
