using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipImagePopup : PopupFactory
{
    public Image shipImage;
    public Text infoText;
    public override void Appear()
    {
        base.Appear();
        infoText.text = "Loading...";
        infoText.gameObject.SetActive(true);
        shipImage.gameObject.SetActive(false);
    }
}
