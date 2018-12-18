using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class Buttons : MonoBehaviour {

	public AcctivePlayer ActivePlayer1;
    GameObject Player;

	void Start () {

		Player = null;
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
