using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterFOG : MonoBehaviour {

    public Canvas GetCanvas;
    public GameObject PlayerMovement;

    void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "Player")
        {
            PlayerMovement.GetComponent<PlayerMovementWorldMap>().StopPlayer();
            GetCanvas.enabled = true;
        }
    }
}
