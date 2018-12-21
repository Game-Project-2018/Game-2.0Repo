using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForSupplies : MonoBehaviour
{

    public void Search(int hour)
    {
        int randomFood = Random.Range(0, 2);
        PlayerMovementWorldMap.instance.food += PlayerMovementWorldMap.instance.teamMembers * randomFood * hour;
        for (int i = 0; i < hour; i++)
        {
            PlayerMovementWorldMap.instance.MovePlayer(1);
        }

        int zombieChanse = hour * 10;
        int random = Random.Range(0, 100);
        if (random >= 0 && random < zombieChanse + PlayerMovementWorldMap.instance.modification)
        {
            PlayerMovementWorldMap.instance.StartZombieAttack();
        }
    }
}
