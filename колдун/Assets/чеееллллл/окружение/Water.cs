using UnityEngine;

public class Water : MonoBehaviour
{
    public float line;
    public GameObject wall;
    public int power;
    int power2;
    void Start()
    {
        if(power != -100)
        {
            power2 = power;
        }
    }
    
    void Update()
    {
        if(power == 0 && power2 != power)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Object floatScript = other.GetComponent<Object>();
        if (other.tag == "Player" && wall != null)
        {
            wall.SetActive(false);
        }
        if (floatScript != null)
        {
            floatScript.isInWater = true;
            floatScript.waterLevel = line;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Object floatScript = other.GetComponent<Object>();
        if (floatScript != null)
        {
            floatScript.isInWater = false;
            floatScript.waterLevel = 0;
        }
        if (other.tag == "Player" && wall != null)
        {
            wall.SetActive(true);
        }
    }
}
