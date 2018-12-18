using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QueueOfUnits : MonoBehaviour {

    public bool Status_ON_OFF;
    public Canvas canvas;
    public Vector2 firstIconPosition;
    [Range(10, 100)]
    public int iconSize;

    [Range(1, 10)]
    public int spaceBetweenIcons;

    public List<Sprite> CharaktersIconSprites;
    public static bool QueueHasChanged = false;

    private GameObject queueGO;
    private List<GameObject> QueueOfIcon = new List<GameObject>();

    
    private List<Sprite> spriteList = new List<Sprite>();
    void Start () {
        if(Status_ON_OFF)
            CreateQueueObject();
    }
	
	void Update () {

	    if (Status_ON_OFF)
	    {
	        if (QueueHasChanged)
	        {
	            QueueOfIcon.Clear();
	            
	            CreateObjectsOfIcons();
	            SetIconSize();
	            SetIconPosition();
                SetIconsSprites();
	            QueueHasChanged = false;
	        }
	    }
	}

    //Tworzy pusty obiekt rodzica w ktorym beda ikony
    void CreateQueueObject() {

        queueGO = new GameObject("UnitsIconsQueue");
        queueGO.transform.SetParent(canvas.transform, false);
    }

    //Tworzy obiekty ikon
    void CreateObjectsOfIcons() {

        GameObject[] go = GameObject.FindGameObjectsWithTag("Icon");

        if (go.Length != 0)
        {
            foreach (GameObject obj in go)
            {
                Destroy(obj);
            }
        }
        
        for (int i = 0; i < TurnManager.GetUnitsInGameList().Count; i++)
        {
            GameObject icon = new GameObject("Icon");
            icon.AddComponent<Image>();
            icon.transform.SetParent(queueGO.transform, false);
            icon.tag = "Icon";
            QueueOfIcon.Add(icon);
        }
    }

    //Ustawia pozycje ikon
    void SetIconPosition() {

        for (int i = 0; i < QueueOfIcon.Count; i++)
        {
            if( i ==  0)
                QueueOfIcon[i].transform.position = new Vector3(firstIconPosition.x, firstIconPosition.y, 0);
            else
                QueueOfIcon[i].transform.position = new Vector3(firstIconPosition.x + (iconSize + spaceBetweenIcons) * i, firstIconPosition.y, 0);
        }
    }

    //Ustawia rozmiar ikon
    void SetIconSize() {

        foreach (GameObject obj in QueueOfIcon)
        {
            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(iconSize, iconSize);
        } 
    }

    //Dodaje Sprite'y do ikon
    void SetIconsSprites() {

        spriteList.Clear();

        List<UnitMovement> units = TurnManager.GetUnitsInGameList();

        for (int i = 0;i < units.Count; i++)
        {
            if (units[i].gameObject.name == "Drunk")
                AddSpritesToSpriteList("Drunk_icon");
            if (units[i].gameObject.name == "Solider")
                AddSpritesToSpriteList("Soldier_icon");
            if (units[i].gameObject.name == "Mother")
                AddSpritesToSpriteList("Mother_icon");
            if (units[i].gameObject.name == "Survivalist")
                AddSpritesToSpriteList("Survivalist_icon");
            if (units[i].gameObject.name == "Doctor")
                AddSpritesToSpriteList("Doctor_icon");
            if (units[i].gameObject.name == "Zombie")
                AddSpritesToSpriteList("Casual_zombie_icon");
            if (units[i].gameObject.name == "Fat Zombie")
                AddSpritesToSpriteList("Fat_zombie_icon");
        }
        AddSpritesToObjects();
    }

    void AddSpritesToSpriteList(string name) {

        foreach (Sprite sprite in CharaktersIconSprites)
        {
            if(sprite.name == name)
                spriteList.Add(sprite);
        }
    }

    void AddSpritesToObjects() {

        for (int i = 0; i < QueueOfIcon.Count; i++)
        {
            QueueOfIcon[i].gameObject.GetComponent<Image>().sprite = spriteList[i];
        }
    }
}
