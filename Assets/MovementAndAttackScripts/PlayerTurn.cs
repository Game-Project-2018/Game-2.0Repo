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

        if (!unitTurn)
        {
            return;
        }

        APlay.Player = gameObject;

        RingCurentUnitTurn.GetUnitPosition(this);

        Weapon.setActivePlayer(gameObject);

        if (RingCurentUnitTurn.ringStatus != true)
        {
            RingCurentUnitTurn.SetActiveRing();
            RingCurentUnitTurn.ringStatus = true;
        }

        if (!alredyMoved)
        {
            if (!moving)
            {
                if (!selectableTilesFinded)
                {
                    FindSelectableTiles();
                    selectableTilesFinded = true;
                }
            }
            else
            {
                CharacterAnimController.isMoving = true;
                Move();
            }
        }

        if (!alredyMoved || !alredyAttack)
        {
            CheckMouse();
        }

        if (alredyMoved)
            CharacterAnimController.isMoving = false;
    }

    public void ButtonEndUnitTurn() {

        alredyMoved = false;
        alredyAttack = false;
        selectableTilesFinded = false;
        RemoveSelectableTiles();
        RingCurentUnitTurn.SetDeactiveRing();
        TurnManager.EndUnitTurn();
    }

    public void ButtonEndTeamTurn() {

        alredyMoved = false;
        alredyAttack = false;
        selectableTilesFinded = false;
        RemoveSelectableTiles();
        RingCurentUnitTurn.SetDeactiveRing();
        TurnManager.ForceEndTeamTurn();
        TurnManager.EndUnitTurn();
    }
}