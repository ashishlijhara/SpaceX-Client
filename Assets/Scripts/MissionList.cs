using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionListItem
{
    public string missionName;
    public int numPayloads;
    public string rocketName;
    public string originCountry;
    public Sprite icon;
    public string[] ships;
}

public class MissionList : AsyncListFactory
{
    public List<MissionListItem> listItems;
#pragma warning disable 0649
    [SerializeField]
    Transform contentPanel;
    [SerializeField]
    ObjectPool pool;
    [SerializeField]
    GameObject loadingText;
#pragma warning restore 0649

    public ShipList shipList; // Hold Reference for children.
    public ShipsPopup shipsPopup; // Hold Reference for children.
    public override void SpawnList()
    {
        loadingText.SetActive(false);
        foreach( MissionListItem listItem in listItems)
        {
            MissionListItem item = listItem;
            GameObject button = pool.RequestAnObject();
            button.transform.SetParent(contentPanel);

            MissionItem missionItem = button.GetComponent<MissionItem>();
            missionItem.Init(item);
        }
    }
}
