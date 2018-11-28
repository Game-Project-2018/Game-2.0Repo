using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player_Creator : MonoBehaviour {



	static int i=0;
	void Start () {
		Mesh drunk = (Mesh)AssetDatabase.LoadAssetAtPath ("Assets/Obj/drunk.obj",typeof(Mesh));
		if (i >= Data_static.Player_tab.Count)
			return;
		else if (Data_static.Player_tab [i].name == "drunk")
			this.GetComponent<MeshFilter> ().mesh = drunk;
	}
	

}
