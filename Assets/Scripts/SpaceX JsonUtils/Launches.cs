using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Launches 
{
    public string mission_name;
    public string launch_date_unix;
    public Rocket rocket;
    public string[] ships;
}
