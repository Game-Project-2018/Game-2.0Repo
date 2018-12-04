using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RingSelectedEnemy : MonoBehaviour {

    private static string unitTag;
    private static Vector3 ringPosition;
    public static float ringStartHeight = 0.6f;

    void Start()
    {
        ringPosition.y = -100;
    }

    void Update()
    {
        if (unitTag != "Player")
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
        ringPosition.y = -100;
    }

    public static void GetEnemyPosition(UnitMovement enemy)
    {
        ringPosition.x = enemy.transform.position.x;
        ringPosition.z = enemy.transform.position.z;
        unitTag = enemy.tag;
    }

    public static Vector3 GetRingPosition()
    {
        return ringPosition;
    }
}
