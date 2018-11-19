using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldForest : MonoBehaviour {

    public int defaultspeed = 4;
    public int affectedspeed = 2;

    void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "Player")
        {
            Player.GetComponent<PlayerMovementWorldMap>().speed = affectedspeed;
        }
    }
    void OnTriggerExit(Collider Player)
    {
        if (Player.tag == "Player")
        {
            Player.GetComponent<PlayerMovementWorldMap>().speed = defaultspeed;
        }
    }
}
