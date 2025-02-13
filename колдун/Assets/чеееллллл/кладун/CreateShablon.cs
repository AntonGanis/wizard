using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateShablon : MonoBehaviour
{
    public GameObject point;

    RegistPoint[] pointi;
    public int don;
    public int start;
    int o;

    public List<GameObject> Objects;

    public bool stepen;
    public int ukazatel;
    public int element;

    Canvas canvas;
    CreatePoint spawn;
    GameObject mat;

    public Transform[] Glifs;
    public Transform nextPoint;

    void Start()
    {
        pointi = new RegistPoint[transform.childCount];
        for (int i = 0; i < pointi.Length; i++) pointi[i] = transform.GetChild(i).GetComponent<RegistPoint>();
        if (don == 0)
        {
            don = (pointi.Length * 100) / 100;
        }
        canvas = GameObject.FindGameObjectWithTag("канвас").GetComponent<Canvas>();
        spawn = FindObjectOfType<CreatePoint>();
        mat = spawn.mat;
        remove();
    }
    public void remove()
    {
        if (pointi != null)
        {
            for (int i = 0; i < pointi.Length; i++)
            {
                pointi[i].yes = false;
                pointi[i].gameObject.SetActive(true);
            }
        }
        start = 0;
    }
    void Update()
    {
        if (o == 20)
        {
            for (int i = 0; i < pointi.Length; i++)
            {
                if (pointi[i].yes == true && pointi[i].gameObject.activeSelf == true)
                {
                    start++;
                    pointi[i].yes = false;
                    pointi[i].gameObject.SetActive(false);
                }
            }
            if (start >= don)
            {
                GameObject _point = Instantiate(point, mat.transform.position, mat.transform.rotation, canvas.transform);
                Objects = new List<GameObject>();
                _point.transform.name = transform.name;
                _point.GetComponent<Glif>().don = don;

                
                for (int i = 0; i < spawn.createdObjects.Count; i++)
                {
                    Objects.Add(spawn.createdObjects[i]);
                    spawn.createdObjects[i].transform.parent = _point.transform;
                    spawn.createdObjects[i].GetComponent<DrawPoint>().end = true;
                }


                _point.GetComponent<Glif>().element = element;
                _point.GetComponent<Glif>().ukazatel = ukazatel;
                _point.GetComponent<Glif>().stepen = stepen;
                spawn.createdObjects.Clear();
                Objects.Clear();
                if (stepen == false)
                {
                    gameObject.SetActive(false);
                }

                for (int i = 0; i < Glifs.Length; i++)
                {
                    Glifs[i].gameObject.SetActive(true);
                    Glifs[i].GetComponent<CreateShablon>().remove();
                    Glifs[i].transform.position = nextPoint.position;
                }
            }
            o = 0;
        }
        else
        {
            o++;
        }
        //if (start < don && o > 10 && Objects.Count > 0)
        //{
        //    spawn.Restart();
        //}
    }
}
