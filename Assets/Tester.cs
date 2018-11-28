using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour {

	void Start(){
		Data_static.Test_number = 0;
	}
	void Update ()
	{
		Data_static.Test_number++;
		Debug.Log (Data_static.Test_number);
	}
}
