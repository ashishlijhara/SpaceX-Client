using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : MonoBehaviour, ListItem<MissionListItem>
{
#pragma warning disable 0649
    [SerializeField]
    Button button;
    [SerializeField]
    Text mission;
    [SerializeField]
    Text payloads;
    [SerializeField]
    Text rocket;
    [SerializeField]
    Text country;
    [SerializeField]
    Image image;
#pragma warning restore 0649
    [HideInInspector]
    public string[] ships;
    MissionList listParent;

    [HideInInspector]
    public List<Coroutine> coroutines;
    List<Coroutine> activeCoroutines;

    public void Init(MissionListItem item)
    {
        listParent = transform.parent.GetComponent<MissionList>();
        mission.text = "Mission Name: " + item.missionName;
        payloads.text = "Number Of Payloads: " + item.numPayloads;
        rocket.text = "Rocket Name: "+item.rocketName;
        country.text = "Country of Origin: " + item.originCountry;
        ships = item.ships;
        image.sprite = item.icon;
        button.onClick.AddListener(LoadShips);
        activeCoroutines = coroutines = new List<Coroutine>();
    }

    public void LoadShips()
    {
        if (ships != null && activeCoroutines.Count == 0)
        {
            listParent.shipsPopup.Appear(this);
            if (ships.Length > 0)
                for (int i = 0; i < ships.Length; i++)
                {
                    Coroutine cor = StartCoroutine(LoadShipAsync(ships[i], i));
                    coroutines.Add(cor);
                    activeCoroutines.Add(cor);
                }
            else
                listParent.shipsPopup.infoText.text = "Ship Data Unavailable";
        }
    }

    public void ClearCoroutines()
    {
        ShipList shipList = listParent.shipList;
        shipList.RefreshList();
        for (int i = 0; i < activeCoroutines.Count; i++)
        {
            StopCoroutine(activeCoroutines[i]);
        }
        coroutines.Clear();
    }

    IEnumerator LoadShipAsync(string ship, int index)
    {
        ShipsJsonFactory shipFactory = new ShipsJsonFactory(this);
        string baseUrl = "https://api.spacexdata.com/v3/ships/";
        ShipList shipList = listParent.shipList;
        shipList.listItems = new List<ShipListItem>();
        shipFactory.FetchJson(baseUrl + ship);
        yield return new WaitUntil(() => shipFactory.IsDone);
        if (!shipFactory.IsError)
        {
            Ships shipsRoot = shipFactory.GetRootObject();
            ShipListItem item = new ShipListItem();
            item.homePort = shipsRoot.ship.home_port;
            item.imageUrl = shipsRoot.ship.image;
            item.numMissions = shipsRoot.ship.missions.Length;
            item.shipName = shipsRoot.ship.ship_name;
            item.shipType = shipsRoot.ship.ship_type;
            shipList.listItems.Add(item);
        }
        shipList.SpawnList();
        if(index>0&& index<coroutines.Count)
            activeCoroutines.RemoveAll(value=>value == coroutines[index]);
    }
}
