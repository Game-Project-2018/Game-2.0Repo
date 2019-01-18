using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckGameStatus : MonoBehaviour {

    public GameObject EndGameObj;

	void Update () {
		CheckIfEndOfBattle();
	}

    public void EndOfGame()
    {
        EndGameObj.SetActive(true);
        StartCoroutine("GoToMenu", 6);
    }

    public void GoToWorldMap()
    {
        Data_static.PlayerWinBattle = true;
        StartCoroutine("GoToWorld", 4);
    }

    public void CheckIfEndOfBattle()
    {
        if (TurnManager.teams["NPC"].Count == 0)
            GoToWorldMap();
        else if (TurnManager.teams["Player"].Count == 0)
            EndOfGame();
    }

    IEnumerator GoToMenu(float time)
    {

        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);
    }

    IEnumerator GoToWorld(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }
}
