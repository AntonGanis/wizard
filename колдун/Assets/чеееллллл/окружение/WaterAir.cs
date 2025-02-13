using UnityEngine;

public class WaterAir : MonoBehaviour
{
    Health obj;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Health>())
        {
            obj = col.gameObject.GetComponent<Health>();
        }
    }
    void Update()
    {
        if (obj != null)
        {
            if (obj.rot.position.y < gameObject.transform.position.y+0.1f)
            {
                if (obj.Ui != null) obj.Ui.SetActive(true);
                obj.inWater = true;
                obj.slider2.gameObject.SetActive(true);
            }
            else
            {
                if (obj.Ui != null) obj.Ui.SetActive(false);
                obj.inWater = false;
            }
        }
    }
}
