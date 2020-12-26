using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform _objTransform;
    private void Awake()
    {
        this._objTransform = gameObject.transform;
    }
    void Start()
    {
        
    }

    void Update()
    {
        controlCamera();
    }



    void controlCamera()
    {
        if (Input.GetKey(KeyCode.D)) { _objTransform.position += new Vector3(0.01f, 0, 0); }
        else if (Input.GetKey(KeyCode.A)) { _objTransform.position += new Vector3(-0.01f, 0, 0); }
        else if (Input.GetKey(KeyCode.W)) { _objTransform.position += new Vector3(0, 0, 0.01f); }
        else if (Input.GetKey(KeyCode.S)) { _objTransform.position += new Vector3(0, 0, -0.01f); }
    }





}
