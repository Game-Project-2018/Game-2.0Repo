using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Select : MonoBehaviour {

	public bool drunk=false,doctor=false, Survivalist=false, Soldier= false,Murderer=false;
    public GameObject drunkG, doctorG, SurvivalistG, SoldierG, MurdererG,MotherG;
    void Start(){
		Data_static.Player_tab = new List <Player_stats> ();
        drunkG.SetActive(false);
        doctorG.SetActive(false);
        SurvivalistG.SetActive(false);
        SoldierG.SetActive(false);
      //  MurdererG.SetActive(false);
        MotherG.SetActive(false);
    }

	public void drunk_toggle(bool newValue)
	{
       
        drunk = newValue;
		if (drunk) {
			Data_static.Player_tab.Add (new Player_stats ("drunk"));
            drunkG.SetActive(true);
        } else {
			for (int i = 0; i < Data_static.Player_tab.Count; i++) {
				if (Data_static.Player_tab [i].name == "drunk")
					Data_static.Player_tab.RemoveAt (i);
			}
            drunkG.SetActive(false);
        }
	}


    public void mother_toggle(bool newValue)
    {

        drunk = newValue;
        if (drunk)
        {
            Data_static.Player_tab.Add(new Player_stats("mother"));
            MotherG.SetActive(true);
        }
        else
        {
            for (int i = 0; i < Data_static.Player_tab.Count; i++)
            {
                if (Data_static.Player_tab[i].name == "mother")
                    Data_static.Player_tab.RemoveAt(i);
            }
            MotherG.SetActive(false);
        }
       

    }

    public void doctor_toggle(bool newValue)
    {
        doctor = newValue;
        if (doctor)
        {
            Data_static.Player_tab.Add(new Player_stats("doctor"));
            doctorG.SetActive(true);
        }
        else
        {
            for (int i = 0; i < Data_static.Player_tab.Count; i++)
            {
                if (Data_static.Player_tab[i].name == "doctor") 
                    Data_static.Player_tab.RemoveAt(i);
            }
            doctorG.SetActive(false);
        }
        
    }

    public void Survivalist_toggle(bool newValue)
    {
        Survivalist = newValue;
        if (Survivalist)
        {
            Data_static.Player_tab.Add(new Player_stats("survivalist"));
            SurvivalistG.SetActive(true);
        }
        else
        {
            for (int i = 0; i < Data_static.Player_tab.Count; i++)
            {
                if (Data_static.Player_tab[i].name == "survivalist") 
                Data_static.Player_tab.RemoveAt(i);
            }
            SurvivalistG.SetActive(false);
        }
        Debug.Log(Data_static.Player_tab[0].name);
    }

    public void Soldier_toggle(bool newValue)
    {
        Soldier = newValue;
        if (Soldier)
        {
            Data_static.Player_tab.Add(new Player_stats("soldier"));
            SoldierG.SetActive(true);
        }
        else
        {
            for (int i = 0; i < Data_static.Player_tab.Count; i++)
            {
                if (Data_static.Player_tab[i].name == "soldier") 
                Data_static.Player_tab.RemoveAt(i);
            }
            SoldierG.SetActive(false);
        }
        Debug.Log(Data_static.Player_tab[0].name);
    }

    public void Murderer_toggle(bool newValue)
    {
        Murderer = newValue;
        if (Murderer)
        {
            Data_static.Player_tab.Add(new Player_stats("murderer"));
            MurdererG.SetActive(true);
        }
        else
        {
            for (int i = 0; i < Data_static.Player_tab.Count; i++)
            {
                if (Data_static.Player_tab[i].name == "murderer")
                    Data_static.Player_tab.RemoveAt(i);
            }
            MurdererG.SetActive(false);
        }
        Debug.Log(Data_static.Player_tab[0].name);
    }

 
}
