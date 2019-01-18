using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour {

    public static Dictionary<string, List<UnitMovement>> teams = new Dictionary<string, List<UnitMovement>>();
    static Queue<string> teamTag = new Queue<string>();
    static Queue<UnitMovement> turnOfTheTeam = new Queue<UnitMovement>();

    static List<UnitMovement> unitsInGameList = new List<UnitMovement>();
    static List<Tile> tileMap = new List<Tile>();
    private bool firstTurn = true;
    private static string currentTeam = "NONE";
    private static string currentUnit = "NONE";
    public static bool forceEndTeamTurn = false;
    public static CheckGameStatus status;

    void Start() {

        GameObject ap = GameObject.FindGameObjectWithTag("ActivePlayer");
        status = ap.GetComponent<CheckGameStatus>();
    }

    void Update() {

        if (turnOfTheTeam.Count == 0) //Jezeli kolejka pusta uruchom Inicjalizacje
        {
            if (firstTurn)
            {
                FirstTurnPlayer();
                firstTurn = false;
            }

            if (unitsInGameList.Count == 0)
            {
                CopyUnitsToTeamsQueue();
            }

            if (teams["Player"].Count != 0 && teams["NPC"].Count != 0)
                InitializationTeamTurnQueue();
            else if (teams["NPC"].Count == 0)
                status.GoToWorldMap();
            else if(teams["Player"].Count == 0)
                status.EndOfGame();
        }
    }

    static void InitializationTeamTurnQueue()
    {
        List<UnitMovement> teamList = teams[teamTag.Peek()];

        currentTeam = teamTag.Peek();

        foreach (UnitMovement unit in teamList)
        {
            turnOfTheTeam.Enqueue(unit);
        }

        StartUnitTurn();
    }

    public static void StartUnitTurn()
    {
        if (turnOfTheTeam.Count > 0)
        {
            currentUnit = turnOfTheTeam.Peek().name;
            ClearMap();
            turnOfTheTeam.Peek().BeginUnitTurn();
        }
    }

    public static void EndTeamTurn()
    {
        if (forceEndTeamTurn)
            forceEndTeamTurn = false;

        UnitsInGameListEndTeamTurn();
        QueueOfUnits.QueueHasChanged = true;
        if (turnOfTheTeam.Count > 0)
            turnOfTheTeam.Clear();
        string team = teamTag.Dequeue();
        teamTag.Enqueue(team);
        InitializationTeamTurnQueue();
    }

    public static void EndUnitTurn()
    {
        UnitMovement unit = turnOfTheTeam.Dequeue();
        unit.EndUnitTurn();

        if(forceEndTeamTurn)
            EndTeamTurn();

        UnitsInGameListEndUnitTurn();
        QueueOfUnits.QueueHasChanged = true;

        if (turnOfTheTeam.Count <= 0)
        {
            EndTeamTurn();
        }
        else
        {
            StartUnitTurn();
        }
    }

    public static void AddUnit(UnitMovement unitMovement)
    {
        List<UnitMovement> list;
        if (!teams.ContainsKey(unitMovement.tag))
        {
            list = new List<UnitMovement>();
            teams[unitMovement.tag] = list;

            if (!teamTag.Contains(unitMovement.tag))
            {
                teamTag.Enqueue(unitMovement.tag);
            }
        }
        else
        {
            list = teams[unitMovement.tag];
        }

        list.Add(unitMovement);
    }

    private void CopyUnitsToTeamsQueue() {

        for (int i = 0; i < teams.Count; i++)
        {
            List<UnitMovement> teamList = teams[teamTag.Peek()];

            foreach (UnitMovement unit in teamList)
            {
                unitsInGameList.Add(unit);
            }

            string team = teamTag.Dequeue();
            teamTag.Enqueue(team);
        }
        QueueOfUnits.QueueHasChanged = true;
    }

    public static void RemoveUnitsFromScene()
    {
        GameObject[] goList = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject go in goList)
        {
            if (go.GetComponent<BaseStats>().HP <= 0)
            {
                Destroy(go);
                go.SetActive(false);
            }

        }

        goList = GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject go in goList)
        {
            if (go.GetComponent<BaseStats>().HP <= 0)
            {
                Destroy(go);
                go.SetActive(false);
            }

        }
    }

    public static void RemoveUnit(UnitMovement unitToRemove)
    {
        List<UnitMovement> teamList;
        List<UnitMovement> teamWithoutRemovedUnit;

        teamList = teams[unitToRemove.tag];
        teamWithoutRemovedUnit = new List<UnitMovement>();

        foreach (UnitMovement unit in teamList)
        {
            if (unit != unitToRemove)
            {
                teamWithoutRemovedUnit.Add(unit);
            }
        }

        teams[unitToRemove.tag] = teamWithoutRemovedUnit;
        UnitsInGameListRemoveUnit(unitToRemove);
    }

    private static void UnitsInGameListEndUnitTurn() {

        UnitMovement unit = unitsInGameList[0];

        for (int i = 0; i < unitsInGameList.Count - 1; i++)
        {
            unitsInGameList[i] = unitsInGameList[i + 1];
        }

        unitsInGameList[unitsInGameList.Count - 1] = unit;
    }

    private static void UnitsInGameListEndTeamTurn() {

        string tag = teamTag.Peek();

        while (unitsInGameList[0].tag == tag)
        {
            UnitsInGameListEndUnitTurn();
        }
    }

    private static void UnitsInGameListRemoveUnit(UnitMovement unitToRemoveFromList)
    {
        List<UnitMovement> temp = new List<UnitMovement>();

        foreach (UnitMovement unit in unitsInGameList)
        {
            if (unit.gameObject != unitToRemoveFromList.gameObject)
                temp.Add(unit);
        }

        unitsInGameList.Clear();
        unitsInGameList = temp;

        QueueOfUnits.QueueHasChanged = true;
    }

    private void FirstTurnPlayer()
    {
        if ((teamTag.Peek() != "Player"))
        {
            string team = teamTag.Dequeue();
            teamTag.Enqueue(team);
        }
    }

    public static void AddTile(Tile tile)
    {
        tileMap.Add(tile);
    }

    private static void ClearMap() {

        foreach (Tile tile in tileMap)
        {
            tile.GetComponent<Renderer>().material.color = Color.white;
        }  
    }

    public static List<String> GetTeamsTagsList() {

        List<String> teamsTagsList = new List<string>();
        for (int i = 0; i < teamTag.Count; i++)
        {
            string tmp = teamTag.Dequeue();
            teamsTagsList.Add(tmp);
            teamTag.Enqueue(tmp);
        }

        return teamsTagsList;
    }

    public static List<UnitMovement> GetUnitsInGameList() {

        return unitsInGameList;
    }

    public static string GetCurrentTeam()
    {
        return currentTeam;
    }

    public static string GetCurrentUnit()
    {
        return currentUnit;
    }

    public static void ForceEndTeamTurn() {
        forceEndTeamTurn = true;
    }

}