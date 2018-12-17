using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurn : UnitMovement
{

    public AcctivePlayer APlay;

    void Start() {

        Initialization();
    }

    void Update() {

        if (UnitLive(this))
        {
            TurnManager.EndUnitTurn();
        }

        if (!unitTurn)
        {
            return;
        }

        APlay.Player = gameObject;

        RingCurentUnitTurn.GetUnitPosition(this);

        if (!alredyMoved)
        {
            if (!moving)
            {
                FindSelectableTiles();
            }
            else
            {
                Move();
            }
        }

        if (!alredyMoved || !alredyAttack)
        {
            CheckMouse();
        }

    }

    public void ButtonPreviousUnit() {

        if (!alredyAttack && !alredyMoved)
        {
            RingCurentUnitTurn.SetDeactiveRing();
            TurnManager.PreviousUnit();
            RemoveSelectableTiles();
        }      
    }

    public void ButtonNextUnit() {

        if (!alredyAttack && !alredyMoved)
        {
            RingCurentUnitTurn.SetDeactiveRing();
            TurnManager.NextUnit();
            RemoveSelectableTiles();
        }   
    }

    public void ButtonEndUnitTurn() {

        alredyMoved = false;
        alredyAttack = false;
        RemoveSelectableTiles();
        RingCurentUnitTurn.SetDeactiveRing();
        TurnManager.EndUnitTurn();
    }

    public void ButtonEndTeamTurn() {

        alredyMoved = false;
        alredyAttack = false;
        RemoveSelectableTiles();
        RingCurentUnitTurn.SetDeactiveRing();
        TurnManager.EndTeamTurn();
    }
}