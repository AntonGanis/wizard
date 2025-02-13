using UnityEngine;

public class Speed : MonoBehaviour
{
    Vector3 lastPosition;
    public float speed;
    Transform obj;
    Damage Dam;
    Rigidbody rb;
    int damag;
    float mass;
    float time;
    void Start()
    {
        obj = transform;
        lastPosition = obj.position;
        Dam = gameObject.GetComponent<Damage>();
        rb = GetComponent<Rigidbody>();
        mass = rb.mass;
    }

    void Update()
    {
        float distanceMoved = Vector3.Distance(lastPosition, obj.position);
        speed = distanceMoved / Time.deltaTime;  

        lastPosition = obj.position;

        if(speed > 70)
        {
            damag = (int)((mass * speed)/30f);
            Dam.valueDown = damag;
        }
        else
        {
            if(time > 0.5f)
            {
                Dam.valueDown = 0;
                time = 0;
            }
            else
            {
                time += Time.deltaTime;

            }
        }

    }
}
