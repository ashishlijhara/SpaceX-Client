using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpaceXJsonClient : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    MissionList missionList;

    [SerializeField]
    Sprite launched;
    [SerializeField]
    Sprite futureLaunch;
#pragma warning restore 0649

    // Start is called before the first frame update
    IEnumerator Start()
    {
        LaunchesJsonFactory launchesJsonFactory = new LaunchesJsonFactory(this);
        launchesJsonFactory.FetchJson("https://api.spacexdata.com/v3/launches");
        yield return new WaitUntil(()=>launchesJsonFactory.IsDone);
        if (!launchesJsonFactory.IsError) {
            Root r = launchesJsonFactory.GetRootObject();
            missionList.listItems = new List<MissionListItem>();
            for(int i=0;i<r.launches.Length;i++)
            {
                MissionListItem listItem = new MissionListItem();
                listItem.missionName = r.launches[i].mission_name;
                listItem.numPayloads = r.launches[i].rocket.second_stage.payloads.Length;
                listItem.rocketName = r.launches[i].rocket.rocket_name;
                listItem.originCountry = r.launches[i].rocket.second_stage.payloads[0].nationality;
                listItem.ships = r.launches[i].ships;
                DateTime date = GetDateTimeFrom(double.Parse(r.launches[i].launch_date_unix));
                listItem.icon = launched;
                if (date > DateTime.Now)
                {
                    listItem.icon = futureLaunch;
                }
                missionList.listItems.Add(listItem);
            }
            missionList.SpawnList();
        }
    }

    private DateTime GetDateTimeFrom(double l)
    {
        System.DateTime unixUTC = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        unixUTC = unixUTC.AddSeconds(l).ToLocalTime();
        return unixUTC;
    }
}
