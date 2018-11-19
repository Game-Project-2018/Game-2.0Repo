using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour {

	public int HP;
    public int MaxHP = 10;
    public int RangeAtack=2;
	public int MeleAtack=5;

	// Use this for initialization
	void Start () {
        HP = MaxHP;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
