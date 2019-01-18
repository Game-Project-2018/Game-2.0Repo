using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollision : MonoBehaviour {

    public bool collisionWithObstacles = false;

    void OnTriggerEnter(Collider Obj)
    {
        if (Obj.tag != "Tile")
            collisionWithObstacles = true;
    }
}
