using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcctivePlayer : MonoBehaviour {
    public Camera cam;
	 public GameObject Player;
	// Use this for initialization
	void Start () {
		Player = null;
       
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (Player != null)
            cam.transform.LookAt(Player.transform.position);
    }
}
