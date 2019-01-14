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

        HPSliderHighlighted.gameObject.SetActive(false);
    }
	
	
	void Update () {

	    if (ActivePlayer.GetComponent<AcctivePlayer>().Player != null)
	    {
	        HPtextPlayer.text = ActivePlayer.GetComponent<AcctivePlayer>().Player.GetComponent<BaseStats>().HP + "/" + ActivePlayer.GetComponent<AcctivePlayer>().Player.GetComponent<BaseStats>().MaxHP;
        }
	    else
	    {
	        HPtextPlayer.text = null;
	    }

	    

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "NPC" || hit.collider.tag == "Player")
            {
                SetValues();
                HPtextHighlighted.text = hit.collider.name + " " + hit.collider.GetComponent<BaseStats>().HP + "/" + hit.collider.GetComponent<BaseStats>().MaxHP;
                HPSliderHighlighted.gameObject.SetActive(true);
                HPSliderHighlighted.maxValue = hit.collider.GetComponent<BaseStats>().MaxHP;
                HPSliderHighlighted.value = hit.collider.GetComponent<BaseStats>().HP;
            }
            else
            {
                HPtextHighlighted.text = null;
                HPSliderHighlighted.gameObject.SetActive(false);
            }
        }
    }

    void SetValues() {

        if (ActivePlayer != null)
        {
            HPSliderPlayer.maxValue = ActivePlayer.GetComponent<AcctivePlayer>().Player.GetComponent<BaseStats>().MaxHP;
            HPSliderPlayer.value = ActivePlayer.GetComponent<AcctivePlayer>().Player.GetComponent<BaseStats>().HP;
        }
    }
}
