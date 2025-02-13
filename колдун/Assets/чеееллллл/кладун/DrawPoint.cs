using UnityEngine;
using UnityEngine.UI;

public class DrawPoint : MonoBehaviour
{
    public Transform last;
    public Transform line;
    public bool end;
    public bool ok;
    Glif par;
    void Start()
    {
        if (last != null)
        {
            Vector3 currentPosition = transform.position;
            Vector3 directionToLast = last.position - currentPosition;
            transform.up = directionToLast.normalized;
            Vector2 linePosition = currentPosition + (directionToLast / 2);
            line.position = linePosition;
            line.up = directionToLast.normalized;
            float distance = directionToLast.magnitude;
            RectTransform lineRectTransform = line.GetComponent<RectTransform>();
            lineRectTransform.sizeDelta = new Vector2(lineRectTransform.sizeDelta.x, distance * 4.5f);
        }
        else
        {
            line.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (transform.parent.name == "Canvas")
        {
            if (Input.GetMouseButtonUp(0))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            par = transform.parent.gameObject.GetComponent<Glif>();
            Color _color = Color.white;
            if (par.ukazatel != 0)
            {
                _color = Color.gray;
            }
            else if (par.stepen == true)
            {
                _color = Color.yellow;
            }
            else if (par.element != 0)
            {
                _color = Color.red;
            }
            gameObject.GetComponent<RawImage>().color = _color;
            line.GetComponent<Image>().color = _color;
        }
    }

}