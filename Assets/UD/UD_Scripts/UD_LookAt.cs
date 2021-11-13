using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_LookAt : MonoBehaviour
{
    private GameObject cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainVirtualCamera");
    }

    void Update()
    {
        gameObject.transform.LookAt(cam.transform);
    }
}
