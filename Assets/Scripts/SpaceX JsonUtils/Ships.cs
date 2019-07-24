using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ships {
    public Ship ship;
}

[System.Serializable]
public class Ship
{
    public string ship_name;
    public string home_port;
    public string ship_type;
    public Mission[] missions;
    public string image;
}

[System.Serializable]
public class Mission
{

}