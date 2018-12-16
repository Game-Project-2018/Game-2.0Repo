using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentTeam : MonoBehaviour {
        
    public Text CurrentTeamText;

    void Update()
    {
        CurrentTeamText.text = TurnManager.GetCurrentTeam();
    }
}
