﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    static Dictionary<string, List<UnitMovement>> teams = new Dictionary<string, List<UnitMovement>>();
    static Queue<string> teamTag = new Queue<string>();
    static Queue<UnitMovement> turnOfTheTeam = new Queue<UnitMovement>();

    static List<UnitMovement> unitsInGameList = new List<UnitMovement>();
    static List<Tile> tileMap = new List<Tile>();

    private bool firstTurn = true;
    private static string currentTeam = "NONE";
    private static string currentUnit = "NONE";

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

            InitializationTeamTurnQueue();
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

    public static void RemoveUnit(UnitMovement unitToRemove) {

        List<UnitMovement> teamList = teams[unitToRemove.tag];
        List<UnitMovement> teamWithoutRemovedUnit = new List<UnitMovement>();

        foreach (UnitMovement unitMovement in teamList)
        {
            if (unitMovement != unitToRemove)
            {
                teamWithoutRemovedUnit.Add(unitMovement);
            }
        }
        teams[unitToRemove.tag] = teamWithoutRemovedUnit;
        Destroy(unitToRemove.gameObject);
        UnitsInGameListRemoveUnit(unitToRemove);
        unitToRemove.gameObject.SetActive(false);
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

    public static void PreviousUnit()
    {
        UnitMovement unit;
        for (int i = 0; i < turnOfTheTeam.Count - 1; i++)
        {
            unit = turnOfTheTeam.Dequeue();
            unit.EndUnitTurn();
            turnOfTheTeam.Enqueue(unit);
        }
        StartUnitTurn();
    }

    public static void NextUnit()
    {
        UnitMovement unit = turnOfTheTeam.Dequeue();
        unit.EndUnitTurn();
        turnOfTheTeam.Enqueue(unit);
        StartUnitTurn();
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

    void EndOfGame()
    {

    }

    void GoToWorldMap()
    {

    }

    public static void CheckIfEndOfBattle(UnitMovement unit)
    {

    }
}