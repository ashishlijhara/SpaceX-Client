using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipsPopup : PopupFactory
{
    public Text infoText;
    MissionItem missionItem; // Item that requested the popup.

    public void Appear(MissionItem item)
    {
        missionItem = item;
        Appear();
    }

    public override void Appear()
    {
        base.Appear();
        infoText.text = "Loading...";
        infoText.gameObject.SetActive(true);
    }

    public override void Dispose()
    {
        missionItem.ClearCoroutines();
        base.Dispose();
    }
}
