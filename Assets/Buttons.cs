using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Buttons : MonoBehaviour {

	public  AcctivePlayer ActivePlayer1;
    GameObject Player;

	void Start () {
		Player = null;
	}

    public void Attack()
    {
        Player = ActivePlayer1.Player;
        Player.GetComponent<PlayerTurn>().ButtonAtack();
    }

    public void Move()
    {
        Player = ActivePlayer1.Player;
        Player.GetComponent<PlayerTurn>().ButtonMove();
    }

    public void PreviousUnit()
    {
        Player = ActivePlayer1.Player;
        Player.GetComponent<PlayerTurn>().ButtonPreviousUnit();
    }

    public void NextUnit()
    {
        Player = ActivePlayer1.Player;
        Player.GetComponent<PlayerTurn>().ButtonNextUnit();
    }

    public void EndUnitTurn()
    {
        Player = ActivePlayer1.Player;
        Player.GetComponent<PlayerTurn>().ButtonEndUnitTurn();
    }

    public void EndTeamTurn()
    {
        Player = ActivePlayer1.Player;
        Player.GetComponent<PlayerTurn>().ButtonEndTeamTurn();
    }
}
