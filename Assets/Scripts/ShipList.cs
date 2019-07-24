using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipListItem
{
    public string shipName;
    public int numMissions;
    public string shipType;
    public string homePort;
    public string imageUrl;
}

public class ShipList : AsyncListFactory
{
    public List<ShipListItem> listItems;
#pragma warning disable 0649
    [SerializeField]
    ShipsPopup shipsPopup;
    [SerializeField]
    ObjectPool pool;
    [SerializeField]
    Transform contentPanel;
#pragma warning restore 0649

    public override void SpawnList()
    {
        shipsPopup.infoText.gameObject.SetActive(false);
        foreach (ShipListItem listItem in listItems)
        {
            ShipListItem item = listItem;
            GameObject panel = pool.RequestAnObject();
            panel.transform.SetParent(contentPanel);

            ShipItem shipItem = panel.GetComponent<ShipItem>();
            shipItem.Init(item);
        }
    }

    public override void RefreshList()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            ShipItem element = toRemove.GetComponent<ShipItem>();
            if (element != null)
                element.Remove();
            pool.StoreInPool(toRemove);
        }
        if(listItems!=null)
            listItems.Clear();
    }
}