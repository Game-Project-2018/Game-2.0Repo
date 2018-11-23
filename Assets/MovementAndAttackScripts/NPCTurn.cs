using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTurn : UnitMovement 
{
    GameObject target;

    // Use this for initialization
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        if (!unitTurn)
        {
            return;
        }

        if (UnitLive(this))
        {
            TurnManager.EndTurn();
        }

        if (!moving)
        {
            FindNearestTarget();
            Atack();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;
        }
        else
        {
            Move();
            if(alredyMoved)
            {
                alredyMoved = false;
                TurnManager.EndTurn();
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

    void Atack()
    {
        Vector3 distance = new Vector3();
        distance = target.transform.position - transform.position;

        if (Mathf.Abs(distance.magnitude) > 1)
            target.GetComponent<BaseStats>().HP -= this.GetComponent<BaseStats>().RangeAtack;
        else
            target.GetComponent<BaseStats>().HP -= this.GetComponent<BaseStats>().MeleAtack;
        if (target.GetComponent<Collider>().GetComponent<BaseStats>().HP <= 0)
        {
            TurnManager.RemoveUnit(this);
        }
    }
}
