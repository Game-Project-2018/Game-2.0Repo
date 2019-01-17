using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player_Creator : MonoBehaviour {



	//static int i=0;
    public GameObject Survivalist;
    public GameObject Drunk;
    public GameObject Doctor;
    public GameObject Mother;
    public GameObject Soldier;
    void Awake() {
         bool drunk = false, doctor = false, survivalist = false, soldier = false, mother = false;
        if (Data_static.Player_tab != null)
        {
            for (int i = 0; i < Data_static.Player_tab.Count; i++)
            {
                if (Data_static.Player_tab[i].name == "survivalist")
                {
                    Activate(Survivalist, i);
                    survivalist = true;
                }
                else if (Data_static.Player_tab[i].name == "doctor")
                {
                    Activate(Doctor, i);
                    doctor = true;
                }
                else if (Data_static.Player_tab[i].name == "drunk")
                {
                    Activate(Drunk, i);
                    drunk = true;
                }
                else if (Data_static.Player_tab[i].name == "soldier")
                {
                    Activate(Soldier, i);
                    soldier = true;
                }
                else if (Data_static.Player_tab[i].name == "mother")
                {
                    Activate(Mother, i);
                    mother = true;
                }

            }
            if (!survivalist)
                Deactivate(Survivalist);
            if (!doctor)
                Deactivate(Doctor);
            if (!drunk)
                Deactivate(Drunk);
            if (!soldier)
                Deactivate(Soldier);
            if (!mother)
                Deactivate(Mother);
            //if (Data_static.Player_tab[i].name != "drunk")

        }
    }

    void Deactivate(GameObject G)
    {
        G.GetComponent<BaseStats>().HP = -1;
        Destroy(G);


    }

    void Activate(GameObject G,int i)
    {
        G.SetActive(true);
        G.GetComponent<BaseStats>().HP = Data_static.Player_tab[i].HP;
        G.GetComponent<BaseStats>().MaxHP = Data_static.Player_tab[i].maxHP;
    }


}
