using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_scroll : MonoBehaviour {
    Camera cam;
   // public GameObject player;
   // GameObject acctivePlayer;
   // public float x;
   // public float z;
    // Use this for initialization
    void Start () {
        cam = this.GetComponent<Camera>();
       // acctivePlayer = player.GetComponent<AcctivePlayer>().Player;

    }
	
	// Update is called once per frame
	void Update () {
     
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) 
        {
            cam.orthographicSize += (Input.GetAxis("Mouse ScrollWheel") * -1);
        }

    }
}
