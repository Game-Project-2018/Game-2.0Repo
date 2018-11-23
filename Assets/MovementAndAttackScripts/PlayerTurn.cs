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
            TurnManager.EndTurn();
        }

        APlay.Player = this.gameObject;

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

    public void ButtonEnd()
    {
        attackTurn = false;
        moveTurn = false;
        alredyMoved = false;
        alredyAttack = false;
		RemoveSelectableTiles ();
        TurnManager.EndTurn();
    }

}