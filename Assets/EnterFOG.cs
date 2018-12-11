using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterFOG : MonoBehaviour {

    public Canvas GetCanvas;



    void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "Player")
        {
            GetCanvas.enabled = true;
        }
    }
}
