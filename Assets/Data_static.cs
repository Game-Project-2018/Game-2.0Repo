using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_stats{
	public int HP=10;
	 public string name; 
	public Player_stats (string name)
	{
		this.name = name;
	}
}
public static class Data_static {
	private static  List<Player_stats> playerTab;
	private static int test_Number=0;

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

	public static int Test_number
	{
		get 
		{
			return test_Number;
		}
		set 
		{
			test_Number = value;
		}



}
}
