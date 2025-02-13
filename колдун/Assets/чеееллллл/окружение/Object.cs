using UnityEngine;

public class Object : MonoBehaviour
{
    public float waterLevel = 0f;
    public float floatStrength = 2f; 

    Rigidbody rb;
    public bool isInWater = false;
    float old;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        old= rb.drag;
    }

    void FixedUpdate()
    {
        if (isInWater)
        {
            float forceFactor = Mathf.Clamp01(waterLevel - transform.position.y);
            Vector3 force = Vector3.up * floatStrength * forceFactor;

            rb.AddForce(force, ForceMode.Acceleration);
            rb.drag = 1;
        }
        else
        {
            rb.drag = old;
        }
    }

}
