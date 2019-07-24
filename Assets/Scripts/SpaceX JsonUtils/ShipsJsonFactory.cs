using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsJsonFactory : SpaceXJsonFactory<Ships>
{
    public ShipsJsonFactory(MonoBehaviour mb)
    {
        SetMonoBehaviour(mb);
    }

    public override string GetJson()
    {
        return "{\"ship\":" + base.GetJson() + "}";
    }
}
