using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatheInWater : MonoBehaviour
{
    bool go;
    Health plar;
    int airMinus;
    public int Minus;
    public int Plus;
    public float time;

    void Start()
    {
        plar = gameObject.GetComponent<Health>();
        airMinus = plar.airMinus;
    }
    void Update()
    {
        if (time>0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            if (go)
            {
                Done(1);
            }
        }
    }
    public void Done(int i)
    {
        go = !go;
        plar.airMinus = plar.airMinus + Minus * i;
        plar.airPlus = plar.airPlus + Plus * i * -1;
    }
}
