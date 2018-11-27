using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RingCurentUnitTurn : MonoBehaviour
{
    public static bool ringVisible = false;
    private static string unitTag;
    private static Vector3 ringPosition;

	void Start()
	{
	    ringPosition.y = transform.position.y;
	}

    void Update()
	{
	    if (unitTag == "Player")
	    {
	        transform.position = ringPosition;
	    }
	}

    public static void GetUnitPosition(UnitMovement unit)
    {
        ringPosition.x = unit.transform.position.x;
        ringPosition.z = unit.transform.position.z;
        unitTag = unit.tag;
    }
}
