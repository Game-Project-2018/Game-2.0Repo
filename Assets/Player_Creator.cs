using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player_Creator : MonoBehaviour {



	static int i=0;
	void Start () {
		//Mesh drunk = (Mesh)AssetDatabase.LoadAssetAtPath ("Assets/Obj/drunk.obj",typeof(Mesh));
		//if (i >= Data_static.Player_tab.Count)
			//return;
		Debug.Log(Data_static.Player_tab.Count);
		if (Data_static.Player_tab.Count == 0) {
			Destroy (GetComponent<GameObject> ());
			Debug.Log("Delete");
		}
		 if (Data_static.Player_tab[i].name != "drunk")
			Destroy(GetComponent<GameObject>());
	}
	

}
