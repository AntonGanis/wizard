using UnityEngine;

public class Damage : MonoBehaviour
{
    public int valueDown;
    public DamageType damageTypes;
    public bool loop;
    int o;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Health>())
        {
            col.GetComponent<Health>().TakeDamage(valueDown, damageTypes);
        }
        if (col.gameObject.GetComponent<Water>() && damageTypes == DamageType.Fire)
        {
            if (col.gameObject.GetComponent<Water>().power != -100) col.gameObject.GetComponent<Water>().power--;
            Destroy(gameObject);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (o > 20)
        {
            if (col.gameObject.GetComponent<Health>() && loop)
            {
                float valueDown2 = valueDown * 0.65f;
                col.GetComponent<Health>().TakeDamage((int)valueDown2 , damageTypes);
            }
            o = 0;
        }
        else
        {
            o++;
        }
    }
}
