using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour {

    public Transform player;
    public Vector3 offset;
    private int ScreenWidth;
    private int ScreenHeight;
    public int Boundary;
    public int speed;
    public int zoomMin;
    public int zoomMax;


    // Use this for initialization
    void Start () {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        transform.position = player.position + offset;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.mousePosition.x > ScreenWidth - Boundary)
        {
            transform.position += new Vector3(Time.deltaTime * speed, 0.0f, 0.0f);
        }
        if (Input.mousePosition.x < Boundary)
        {
            transform.position += new Vector3(-Time.deltaTime * speed, 0.0f, 0.0f);
        }
        if (Input.mousePosition.y > ScreenHeight - Boundary)
        {
            transform.position += new Vector3(0.0f, 0.0f, Time.deltaTime * speed);
        }
        if (Input.mousePosition.y < Boundary)
        {
            transform.position += new Vector3(0.0f, 0.0f, -Time.deltaTime * speed);
        }

        //zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (GetComponent<Camera>().fieldOfView > zoomMin)
            {
                GetComponent<Camera>().fieldOfView--;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (GetComponent<Camera>().fieldOfView < zoomMax)
            {
                GetComponent<Camera>().fieldOfView++;
            }
        }
    }
}
