using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHP : MonoBehaviour
{
    public static String unitHP;

	void Update()
	{
	    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    RaycastHit hit;
	    if (Physics.Raycast(ray, out hit))
	    {
	        if (hit.collider.tag == "NPC" || hit.collider.tag == "Player")
	        {
	            unitHP = hit.collider.tag + " " + hit.collider.GetComponent<BaseStats>().HP + "/" + hit.collider.GetComponent<BaseStats>().MaxHP;
	        }
	    }
    }
}
