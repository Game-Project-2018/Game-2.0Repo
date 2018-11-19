using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : TacticsMove
{
    bool AtackTurn = false;
    bool MoveTurn = false;
	bool Alredy_atack=false;
    bool Alredy_moved = false;
    void Start () 
	{
        Init();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            return;
        }
        if (MoveTurn == true)
        {
            if (!moving && !Alredy_moved)
            {
                FindSelectableTiles();
                CheckMouse();
            }
            else 
            {
                Move();
                Alredy_moved = true;
            }
        }
        else
        {
            CheckMouse();
        }
	}

   public void ButtonAtack()
    {
        AtackTurn = true;
        MoveTurn = false;
    }

    public void ButtonMove()
    {
        AtackTurn = false;
        MoveTurn = true;
    }

   public void ButtonEnd()
    {
        Alredy_moved = false;
        Alredy_atack = false;
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
				if (hit.collider.tag == "Tile") {
					Tile t = hit.collider.GetComponent<Tile> ();

					if (t.selectable) {
						t.GetComponent<Renderer>().material.color = Color.green;
						MoveToTile (t);

						
					}
				}
				else if (hit.collider.tag == "NPC" && Alredy_atack==false) {
                    Vector3 distance = new Vector3();
                    distance = hit.collider.transform.position - transform.position;
                    if (Mathf.Abs(distance.magnitude)>1)
						hit.collider.GetComponent<BaseStats> ().HP -= this.GetComponent<BaseStats> ().RangeAtack;
                    else
                        hit.collider.GetComponent<BaseStats>().HP -= this.GetComponent<BaseStats>().MeleAtack;
                    Alredy_atack = true;
                    if (hit.collider.GetComponent<BaseStats>().HP <= 0)
                    {
                        Destroy(hit.collider.gameObject);

                        
                    }
                }
            }
        }
    }
}
