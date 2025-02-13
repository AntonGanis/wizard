using UnityEngine;

public class Tolkat : MonoBehaviour
{
    public float pushForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 pushDirection = collision.transform.position - transform.position;
            pushDirection.Normalize();
            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}
