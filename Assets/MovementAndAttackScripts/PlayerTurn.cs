using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : UnitMovement
{
    bool attackTurn = false;
    bool moveTurn = false;
    public AcctivePlayer APlay;

    void Start()
    {
        Initialization();
    }

    void Update()
    {

        if (!unitTurn)
        {
            return;
        }

        if (UnitLive(this))
        {
            TurnManager.EndUnitTurn();
        }

        APlay.Player = gameObject;

        if (moveTurn && !alredyMoved)
        {
            if (!moving)
            {
                FindSelectableTiles();
                CheckMouse();
            }
            else
            {
                Move();
            }
        }

        if (attackTurn && !alredyAttack)
        {
            CheckMouse();
        }

        if (alredyMoved && alredyAttack)
        {
            TurnManager.EndUnitTurn();
        }
    }

    public void ButtonAtack()
    {
        attackTurn = true;
        moveTurn = false;
    }

    public void ButtonMove()
    {
        attackTurn = false;
        moveTurn = true;
    }

    public void ButtonPreviousUnit()
    {
        if (!attackTurn && !moveTurn)
            TurnManager.PreviousUnit();
    }

    public void ButtonNextUnit()
    {
        if (!attackTurn && !moveTurn)
            TurnManager.NextUnit();
    }

    public void ButtonEndUnitTurn()
    {
        attackTurn = false;
        moveTurn = false;
        alredyMoved = false;
        alredyAttack = false;
        RemoveSelectableTiles();
        TurnManager.EndUnitTurn();
    }

    public void ButtonEndTeamTurn()
    {
        attackTurn = false;
        moveTurn = false;
        alredyMoved = false;
        alredyAttack = false;
        RemoveSelectableTiles();
        TurnManager.EndTeamTurn();
    }
}