using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSurf : MonoBehaviour
{
    public bool toStopRotate;

    Quaternion rotation;

    void Awake()
    {
        rotation = transform.rotation;
    }

    void LateUpdate()
    {
        //if(toStopRotate)
        //{
        //    transform.rotation = rotation;
        //}
    }
}
