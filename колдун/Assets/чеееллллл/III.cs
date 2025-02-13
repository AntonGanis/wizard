using UnityEngine;

public class III : MonoBehaviour
{
    public Transform obj;
    public bool rot;
    void Update()
    {
        transform.position = obj.position;
        if (rot)
        {
            transform.rotation = obj.rotation;
        }
    }
}
