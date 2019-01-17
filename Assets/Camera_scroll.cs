using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_scroll : MonoBehaviour {
    Camera cam;
    // Use this for initialization
    void Start () {
        cam = this.GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) 
        {
            cam.orthographicSize += (Input.GetAxis("Mouse ScrollWheel") * -1);
        }

    }
}
