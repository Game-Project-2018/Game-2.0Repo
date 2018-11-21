using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Buttons : MonoBehaviour {

	public  AcctivePlayer ActivePlayer1;
    GameObject Player;

	void Start () {
		Player = null;
	}

	public   void Atack()
	{
		Player = ActivePlayer1.Player;
		Player.GetComponent<PlayerTurn> ().ButtonAtack ();
	}

	public void Move()
	{
		Player = ActivePlayer1.Player;
		Player.GetComponent<PlayerTurn> ().ButtonMove ();
	}

	public void End()
	{
		Player = ActivePlayer1.Player;
		Player.GetComponent<PlayerTurn> ().ButtonEnd ();
	}


}
