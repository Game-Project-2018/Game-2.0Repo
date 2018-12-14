using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Select : MonoBehaviour {

	public bool drunk=false,doctor=false, Survivalist=false, Soldier= false,Murderer=false;

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
                if (Data_static.Player_tab[i].name == "doctor") 
                    Data_static.Player_tab.RemoveAt(i);
            }
        }
        Debug.Log(Data_static.Player_tab[0].name);
    }

    public void Survivalist_toggle(bool newValue)
    {
        Survivalist = newValue;
        if (Survivalist)
        {
            Data_static.Player_tab.Add(new Player_stats("survivalist"));
        }
        else
        {
            for (int i = 0; i <= Data_static.Player_tab.Count; i++)
            {
                if (Data_static.Player_tab[i].name == "survivalist") 
                Data_static.Player_tab.RemoveAt(i);
            }
        }
        Debug.Log(Data_static.Player_tab[0].name);
    }

    public void Soldier_toggle(bool newValue)
    {
        Soldier = newValue;
        if (Soldier)
        {
            Data_static.Player_tab.Add(new Player_stats("soldier"));
        }
        else
        {
            for (int i = 0; i <= Data_static.Player_tab.Count; i++)
            {
                if (Data_static.Player_tab[i].name == "soldier") 
                Data_static.Player_tab.RemoveAt(i);
            }
        }
        Debug.Log(Data_static.Player_tab[0].name);
    }

    public void Murderer_toggle(bool newValue)
    {
        Murderer = newValue;
        if (Murderer)
        {
            Data_static.Player_tab.Add(new Player_stats("murderer"));
        }
        else
        {
            for (int i = 0; i <= Data_static.Player_tab.Count; i++)
            {
                if (Data_static.Player_tab[i].name == "murderer")
                    Data_static.Player_tab.RemoveAt(i);
            }
        }
        Debug.Log(Data_static.Player_tab[0].name);
    }

 
}
