using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCity : MonoBehaviour {

    public Canvas GetCanvas;

    void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "Player")
        {
            Player.GetComponent<PlayerMovementWorldMap>().speed = 1;
            GetCanvas.enabled = true;
        }
    }
    void OnTriggerExit(Collider Player)
    {
        if (Player.tag == "Player")
        {
            Player.GetComponent<PlayerMovementWorldMap>().speed = 4;
            GetCanvas.enabled = false;
        }
    }
}
