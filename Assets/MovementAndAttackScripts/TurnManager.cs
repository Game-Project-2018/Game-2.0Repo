using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<UnitMovement>> teams = new Dictionary<string, List<UnitMovement>>();
    static Queue<string> teamTag = new Queue<string>();
    static Queue<UnitMovement> turnOfTheTeam = new Queue<UnitMovement>();

    private bool firstTurn = true;
    public static string currentTeam;
    public static string currentUnit;

    void Update()
    {
        if (turnOfTheTeam.Count == 0) //Jezeli kolejka pusta uruchom Inicjalizacje
        {
            if (firstTurn)
            {
                FirstTurnPlayer();
                firstTurn = false;
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
            turnOfTheTeam.Peek().BeginUnitTurn();
        }
    }

    public static void EndTeamTurn()
    {
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

    public static void RemoveUnit(UnitMovement unit)
    {
        List<UnitMovement> newTeamList = new List<UnitMovement>();
        UnitMovement tempUnit = new UnitMovement();
        foreach (String tag in teamTag)
        {
            foreach (UnitMovement unitMovement in teams[tag])
            {
                if (unitMovement.GetComponent<BaseStats>().HP <= 0)
                {
                    tempUnit = unitMovement;
                }
            }
        }

        foreach (UnitMovement unitMovement in teams[tempUnit.tag])
        {
            if (unitMovement.GetComponent<BaseStats>().HP > 0)
            {
                newTeamList.Add(unitMovement);
            }
        }

        teams[tempUnit.tag].Clear();
        teams[tempUnit.tag] = newTeamList;
        Destroy(tempUnit.gameObject);
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

    void FirstTurnPlayer()
    {
        if ((teamTag.Peek() != "Player"))
        {
            string team = teamTag.Dequeue();
            teamTag.Enqueue(team);
        }
    }
}