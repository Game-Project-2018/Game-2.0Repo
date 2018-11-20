using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour 
{
    static Dictionary<string, List<UnitMovement>> units = new Dictionary<string, List<UnitMovement>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<UnitMovement> turnTeam = new Queue<UnitMovement>();

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
	}

    static void InitTeamTurnQueue()
    {
        List<UnitMovement> teamList = units[turnKey.Peek()];

        foreach (UnitMovement unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }

        StartTurn();
    }

    public static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginUnitTurn();
        }
    }

    public static void EndTurn()
    {
        UnitMovement unit = turnTeam.Dequeue();
        unit.EndUnitTurn();

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    public static void AddUnit(UnitMovement unit)
    {
        List<UnitMovement> list;

        if (!units.ContainsKey(unit.tag))
        {
            list = new List<UnitMovement>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }


    public static void RemoveUnit(UnitMovement unit)
    {
        List<UnitMovement> list;


            list = units[unit.tag];


        list.Remove(unit);
    }

}
