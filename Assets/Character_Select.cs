using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Select : MonoBehaviour {

	public bool drunk=false,doctor=false;



	public void drunk_toggle(bool newValue)
	{
	    drunk = newValue;
	}

	public void StartGame()
	{
		Data_static.Player_tab = new List <Player_stats> ();
		if (drunk) {
			Data_static.Player_tab.Add(new Player_stats("drunk"));
		}
		if (doctor) {
			Data_static.Player_tab.Add(new Player_stats("doctor"));
		}
	}
}
