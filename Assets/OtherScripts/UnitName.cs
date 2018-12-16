using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitName : MonoBehaviour
{
    public Text unitName;

    void Update()
    {
        unitName.text = TurnManager.GetCurrentUnit();
    }
}
