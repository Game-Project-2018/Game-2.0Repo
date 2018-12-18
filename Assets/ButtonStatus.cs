using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStatus : MonoBehaviour
{
    public List<Button> buttons;
	
	void Update () {

	    foreach (Button button in buttons)
	    {
	        if( TurnManager.GetCurrentTeam() != "Player" )
	            button.gameObject.SetActive(false);
	        else if( button.gameObject.activeSelf != true )
	            button.gameObject.SetActive(true);
	    }
    }
}
