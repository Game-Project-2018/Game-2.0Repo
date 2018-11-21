using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : UnitMovement
{
    bool attackTurn = false;
    bool moveTurn = false;
    bool alredyAttack = false;
    bool alredyMoved = false;
	public AcctivePlayer APlay;
    void Start()
    {
        Initialization();
    }

    void Update()
    {
		
		if (!unitTurn) {
			return;
		} else {
			APlay.Player = this.gameObject;
		}
        //if(alredyMoved && alredyAttack)

        if (moveTurn)
        {
            if (!moving && !alredyMoved && !reachTarget)
            {
                FindSelectableTiles();
                CheckMouse();
            }
            else
            {
                Move();
                if (reachTarget)
                {
                    alredyMoved = true;
                }
          
                    //TurnManager.EndTurn(); //moveTurn = false;
            }
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
        alredyMoved = false;
        alredyAttack = false;
        reachTarget = false;
        TurnManager.EndTurn();
    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        t.GetComponent<Renderer>().material.color = Color.green;
                        MoveToTile(t);
                    }
                }
            }
        }
    }
}