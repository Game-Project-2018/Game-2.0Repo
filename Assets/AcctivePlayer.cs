using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcctivePlayer : MonoBehaviour {
    public Camera cam;
	public GameObject Player;

	void Start () {
		Player = null;
	}
	
	void LateUpdate ()
	{
	    if (Player != null)
	        cam.transform.LookAt(Player.transform.position);
	}
}
