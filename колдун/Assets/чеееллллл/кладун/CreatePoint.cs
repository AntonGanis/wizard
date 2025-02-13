using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePoint : MonoBehaviour
{
    public GameObject spritePrefab;
    public float dist = 5;
    Canvas canvas;
    public List<GameObject> createdObjects = new List<GameObject>();
    public bool comp;
    public GameObject mat;

    public bool isLocked;
    MouseLook cam;
    MouseLook plarCam;
    Final2 fin;
    public GameObject Lins;

    public Transform[] Glifs;
    public Transform[] Glifs2;
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("канвас").GetComponent<Canvas>();
        fin = FindObjectOfType<Final2>();
        plarCam = gameObject.GetComponent<MouseLook>();
        cam = transform.GetChild(0).GetComponent<MouseLook>();
        SetCursorLock(true);
    }
    void OffGlifs(bool isLocked)
    {
        for (int i = 0; i < Glifs.Length; i++)
        {
            Glifs[i].gameObject.SetActive(!isLocked);
            Glifs[i].transform.position = mat.transform.position;
            Glifs[i].GetComponent<CreateShablon>().remove();
        }
        for (int i = 0; i < Glifs2.Length; i++)
        {
            Glifs2[i].gameObject.SetActive(false);
            Glifs2[i].GetComponent<CreateShablon>().remove();
        }
    }
    void SetCursorLock(bool isLocked)
    {
        this.isLocked = isLocked;
        Screen.lockCursor = isLocked;
        Cursor.visible = !isLocked;
        plarCam.enabled = isLocked;
        cam.enabled = isLocked;
        fin.gameObject.SetActive(!isLocked);
        Lins.SetActive(!isLocked);
        OffGlifs(isLocked);
        if (isLocked == true)
        {
            fin.END = false;
        }

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(2) || fin.END == true)
        {
            SetCursorLock(!isLocked);
        }
        if (Input.GetMouseButtonUp(0) && isLocked == false)
        {
            OffGlifs(isLocked);
        }
        if (isLocked == false)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Input.mousePosition;

                Vector2 localPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), mousePos, canvas.worldCamera, out localPos);
                if (comp == true)
                {
                    mat.transform.localPosition = localPos;
                    createdObjects.Clear();
                    comp = false;
                }
                if (createdObjects.Count > 0)
                {
                    Vector2 lastObjectPos = createdObjects[createdObjects.Count - 1].GetComponent<RectTransform>().anchoredPosition;
                    float distance = Vector2.Distance(localPos, lastObjectPos);

                    if (distance > dist && distance < dist*7)
                    {
                        Create_Point(localPos);
                    }
                }
                else
                {
                    Create_Point(localPos);
                }
            }
            else
            {
                comp = true;
            }
        }
    }

    void Create_Point(Vector2 position)
    {
        GameObject spriteInstance = Instantiate(spritePrefab, canvas.transform);
        spriteInstance.GetComponent<RectTransform>().anchoredPosition = position;
        createdObjects.Add(spriteInstance);
        if (createdObjects.Count - 1 > 0)
        {
            spriteInstance.GetComponent<DrawPoint>().last = createdObjects[createdObjects.Count - 2].transform;
        }
    }
    public void Restart()
    {
        for (int i = 0; i < createdObjects.Count; i++)
        {
            Destroy(createdObjects[i]);
        }
        createdObjects.Clear();
    }
}
