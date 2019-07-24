using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ShipItem : MonoBehaviour, ListItem<ShipListItem>
{
    public Button button;
    public Text missions;
    public Text shipName;
    public Text type;
    public Text port;
    [HideInInspector]
    public string imageUrl;
    public GameObject imageDisplay;
    GameObject popup;

    public void Init(ShipListItem item)
    {
        missions.text = "Number of Missions: " + item.numMissions;
        type.text = "Ship Type: " + item.shipType;
        shipName.text = "Ship Name: " + item.shipName;
        port.text = "Home Port: " + item.homePort;
        imageUrl = item.imageUrl;
        button.onClick.AddListener(LoadShipImage);
    }

    public void Remove()
    {
        button.onClick.RemoveListener(LoadShipImage);
    }

    void LoadShipImage()
    {
        popup = Instantiate(imageDisplay, GameObject.FindGameObjectWithTag("Canvas").transform) as GameObject;
        ShipImagePopup shipImPop = popup.GetComponent<ShipImagePopup>();
        shipImPop.Appear();
        if (imageUrl != null)
            StartCoroutine(LoadShipImageAsync());
        else
            shipImPop.infoText.text = "Image Unavailable";
    }

    IEnumerator LoadShipImageAsync()
    {
        ShipImagePopup shipPopup = popup.GetComponent<ShipImagePopup>();
        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return imageRequest.SendWebRequest();
        if (imageRequest.isNetworkError || imageRequest.isHttpError)
            shipPopup.infoText.text = "Unable To Load Image";
        else
        {
            shipPopup.infoText.gameObject.SetActive(false);
            shipPopup.shipImage.sprite = SpriteFromTexture2D(((DownloadHandlerTexture)imageRequest.downloadHandler).texture);
            shipPopup.shipImage.gameObject.SetActive(true);
        }
    }

    Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
