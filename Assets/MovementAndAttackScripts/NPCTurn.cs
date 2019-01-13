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

        if (!moving)
        {
            target = FindNearestTarget();
            if (target != null)
            {
                Atack();
                CalculatePath();
                FindSelectableTiles();
                actualTargetTile.target = true;
            }
        }
        else if(target != null)
        {
            Move();

            if (alredyMoved)
            {
                if (!alredyAttack)
                {
                    Atack();
                }
                alredyAttack = false;
                alredyMoved = false;
                TurnManager.EndUnitTurn();
            }
        }
    }

    void CalculatePath() {

        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    void Atack()
    {
        Vector3 distance = new Vector3();
        distance = target.transform.position - transform.position;

        if (Mathf.Abs(distance.magnitude) > 1)
        {
            target.GetComponent<BaseStats>().HP -= this.GetComponent<BaseStats>().RangeAtack;
            alredyAttack = true;
        }
        else
        {
            target.GetComponent<BaseStats>().HP -= this.GetComponent<BaseStats>().MeleAtack;
            alredyAttack = true;
        }

        UnitMovement unit = target.GetComponent<UnitMovement>();

        if (unit.GetComponent<BaseStats>().HP <= 0)
            UnitIsAlive(unit);
    }
}
