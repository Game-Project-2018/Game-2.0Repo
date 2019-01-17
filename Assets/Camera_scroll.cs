using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_scroll : MonoBehaviour {
    Camera cam;

    public float min = 2;
    public float max = 7;

    void Start () {
        cam = this.GetComponent<Camera>();
    }
	
	void Update () {
     
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) 
        {
            if(cam.orthographicSize + (Input.GetAxis("Mouse ScrollWheel") * -1) > min && cam.orthographicSize + (Input.GetAxis("Mouse ScrollWheel") * -1) < max)
                cam.orthographicSize += (Input.GetAxis("Mouse ScrollWheel") * -1);
        }

    }
}
