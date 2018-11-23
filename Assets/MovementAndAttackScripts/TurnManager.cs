using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<UnitMovement>> teams = new Dictionary<string, List<UnitMovement>>();
    static Queue<string> teamTag = new Queue<string>();
    static Queue<UnitMovement> turnOfTheTeam = new Queue<UnitMovement>();
    static Queue<UnitMovement> units = new Queue<UnitMovement>();

    private bool firstTurn = true;
    public static string currentTeam;

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

        StartTurn();
    }

    public static void StartTurn()
    {
        if (turnOfTheTeam.Count > 0)
        {
            turnOfTheTeam.Peek().BeginUnitTurn();
        }
    }

    public static void EndTurn()
    {
        UnitMovement unitMovement = turnOfTheTeam.Dequeue();
        unitMovement.EndUnitTurn();

        if (turnOfTheTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = teamTag.Dequeue();
            teamTag.Enqueue(team);
            InitializationTeamTurnQueue();
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
        units.Enqueue(unitMovement);
    }

    public static void RemoveUnit(UnitMovement unit) //TEST
    {
        List<UnitMovement> list = teams[unit.tag];

        foreach (UnitMovement unitMovement in list)
        {
            if (unitMovement.GetComponent<BaseStats>().HP <= 0)
                list.Remove(unit);
        }
    }

    public static void CheckIfUnitIsAlive() //TEST
    {
        foreach (UnitMovement unit in units)
        {
            if (unit.GetComponent<BaseStats>().HP <= 0)
            {
                RemoveUnit(unit);
                units.Enqueue(unit);
            }
        }
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