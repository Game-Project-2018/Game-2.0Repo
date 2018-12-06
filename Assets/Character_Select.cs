using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Select : MonoBehaviour {

	public bool drunk=false,doctor=false;

	void Start(){
		Data_static.Player_tab = new List <Player_stats> ();
	}

	public void drunk_toggle(bool newValue)
	{
	    drunk = newValue;
		if (drunk) {
			Data_static.Player_tab.Add (new Player_stats ("drunk"));
		} else {
			for (int i = 0; i <= Data_static.Player_tab.Count; i++) {
				if (Data_static.Player_tab [i].name == "drunk")
					Data_static.Player_tab.RemoveAt (i);
			}
		}
		Debug.Log (Data_static.Player_tab [0].name);
	}

    public void doctor_toggle(bool newValue)
    {
        doctor = newValue;
        if (doctor)
        {
            Data_static.Player_tab.Add(new Player_stats("doctor"));
        }
        else
        {
            for (int i = 0; i <= Data_static.Player_tab.Count; i++)
            {
                if (Data_static.Player_tab[i].name == "doctor") ;
                    Data_static.Player_tab.RemoveAt(i);
            }
        }
        Debug.Log(Data_static.Player_tab[0].name);
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
