using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTurn : UnitMovement 
{
    GameObject target;
    bool alredyAtack = false;
    bool alredyMoved = false;

    void Start () 
	{
        Initialization();
	}
	
	void Update () 
	{

        if (!unitTurn)
        {
            return;
        }

        if (!moving && !alredyMoved)
        {
            FindNearestTarget();
            //Atack();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;
        }
        else
        {
            Move();

            if (reachTarget)
            {
                reachTarget = false;
                TurnManager.EndTurn();
                //alredyMoved = true;
            }
        }
	    
    }

    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);

            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
    }

    void Atack ()
    {

        Vector3 distance = new Vector3();
        distance = target.transform.position - transform.position;

        if (Mathf.Abs(distance.magnitude) > 1)
            target.GetComponent<BaseStats>().HP -= this.GetComponent<BaseStats>().RangeAtack;
        else
            target.GetComponent<BaseStats>().HP -= this.GetComponent<BaseStats>().MeleAtack;
    }

}
