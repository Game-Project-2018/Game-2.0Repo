using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour 
{
    int rotation = 0;
    public int rotation_speed = 1;

    private void Update()
    {
        if (rotation > 0)
        {
            transform.Rotate(Vector3.up, rotation_speed, Space.Self);
            rotation -= rotation_speed;
        }
        if (rotation < 0)
        {
            transform.Rotate(Vector3.up, -rotation_speed, Space.Self);
            rotation += rotation_speed;
        }
    }


    public void RotateLeft()
    {
        rotation += 90;
    }

    public void RotateRight()
    {
        rotation -= 90;
    }
}
