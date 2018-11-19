using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighLighting : MonoBehaviour {
        
        public Text hpText;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "NPC" || hit.collider.tag == "Player")
            {
                hpText.text = hit.collider.tag + " "  + hit.collider.GetComponent<BaseStats>().HP.ToString() + "/" + hit.collider.GetComponent<BaseStats>().MaxHP.ToString();
            }

        }
    }
}
