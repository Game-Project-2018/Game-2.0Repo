using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpShow : MonoBehaviour {

    public Slider HPSliderPlayer;
    public Text HPtextPlayer;

    public Slider HPSliderHighlighted;
    public Text HPtextHighlighted;

    public GameObject ActivePlayer;


    void Start () {
		
	}
	
	
	void Update () {
        HPSliderPlayer.maxValue = ActivePlayer.GetComponent<AcctivePlayer>().Player.GetComponent<BaseStats>().MaxHP;
        HPSliderPlayer.value = ActivePlayer.GetComponent<AcctivePlayer>().Player.GetComponent<BaseStats>().HP;
        HPtextPlayer.text = ActivePlayer.GetComponent<AcctivePlayer>().Player.GetComponent<BaseStats>().HP.ToString() + " /" + ActivePlayer.GetComponent<AcctivePlayer>().Player.GetComponent<BaseStats>().MaxHP;
    }
}
