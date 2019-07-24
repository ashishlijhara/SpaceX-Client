using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LaunchesJsonFactory : SpaceXJsonFactory<Root>
{
    public LaunchesJsonFactory(MonoBehaviour mb)
    {
        SetMonoBehaviour(mb);
    }

    public override string GetJson()
    {
        return "{\"launches\":" + base.GetJson() + "}";
    }
}
