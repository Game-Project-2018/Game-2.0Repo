using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_stats{
	public int HP=10;
    public int maxHP = 10;
    public int DistanceDMG = 2;
    public int MeleDMG = 3;
	public string name; 
	public Player_stats (string name) {
		this.name = name;
	}
}
public static class Data_static {

    private static  List<Player_stats> playerTab;
    public static bool PlayerWinBattle { get; set; }
    public static Vector3 GlobalPlayerPosition { get; set; }

    public static List<Player_stats> Player_tab
	{
		get 
		{
			return playerTab;
		}
		set 
		{
			playerTab = value;
		}
	}
}

