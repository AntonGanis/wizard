using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : MonoBehaviour
{
    public GameObject papper;
    GameObject camera;
    GameObject plar;

    void Start()
    {
        papper.SetActive(false);
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        plar = FindObjectOfType<Move>().gameObject;

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Move>())
        {
            col.gameObject.GetComponent<Move>().enabled = false;
            col.gameObject.GetComponent<MouseLook>().enabled = false;
            camera.GetComponent<MouseLook>().enabled = false;
            papper.SetActive(true);
            Screen.lockCursor = false;
            Cursor.visible = true;
        }
    }
    public void exit()
    {
        plar.GetComponent<Move>().enabled = true;
        plar.GetComponent<MouseLook>().enabled = true;
        camera.GetComponent<MouseLook>().enabled = true;
        Screen.lockCursor = true;
        Cursor.visible = false;
        papper.SetActive(false);
    }
}
