using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public class RingCurentUnitTurn : MonoBehaviour
{
    private static string unitTag;
    private static Vector3 ringPosition;
    public static float ringStartHeight = 0.6f;
    public static bool ringStatus = false;

    void Start()
	{
	    ringPosition.y = ringStartHeight;
	}

    void Update()
	{
	    if (unitTag == "Player")
	    {
	        transform.position = ringPosition;
	    }
	}

    public static void SetActiveRing()
    {
        ringPosition.y = ringStartHeight;
    }

    public static void SetDeactiveRing()
    {
        ringStatus = false;
        ringPosition.y = -100;
    }

    public static void GetUnitPosition(UnitMovement unit)
    {
        ringPosition.x = unit.transform.position.x;
        ringPosition.z = unit.transform.position.z;
        unitTag = unit.tag;
    }
}
