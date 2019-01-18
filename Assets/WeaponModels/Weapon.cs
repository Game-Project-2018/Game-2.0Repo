using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject GunDoctor;
    public GameObject GunDrunk;
    public GameObject GunMother;
    public GameObject GunSolider;
    public GameObject GunSurvivalist;

    private bool gunDoctor;
    private bool gunDrunk;
    private bool gunMother;
    private bool gunSolider;
    private bool gunSurvivalist;

    private GameObject activePlayer;

	void Update () {
		
        if(gunDoctor)
            GunDoctor.SetActive(true);
        else if(gunDoctor!=null)
            GunDoctor.SetActive(false);

	    if (gunDrunk)
	        GunDrunk.SetActive(true);
	    else if(gunDrunk != null)
            GunDrunk.SetActive(false);

	    if (gunMother)
	        GunMother.SetActive(true);
	    else if (gunMother != null)
            GunMother.SetActive(false);

	    if (gunSolider)
	        GunSolider.SetActive(true);
	    else if (gunSolider != null)
            GunSolider.SetActive(false);

	    if (gunSurvivalist)
	        GunSurvivalist.SetActive(true);
	    else if (gunSurvivalist != null)
            GunSurvivalist.SetActive(false);
    }

    public void setGunVisible(bool gunStatus)
    {
        if (activePlayer.name == "Doctor")
            gunDoctor = gunStatus;
        if (activePlayer.name == "Drunk")
            gunDrunk = gunStatus;
        if (activePlayer.name == "Mother")
            gunMother = gunStatus;
        if (activePlayer.name == "Solider")
            gunSolider = gunStatus;
        if (activePlayer.name == "Survivalist")
            gunSurvivalist = gunStatus;
    }

    public void setActivePlayer(GameObject activePlayer)
    {
        this.activePlayer = activePlayer;
    }
}
