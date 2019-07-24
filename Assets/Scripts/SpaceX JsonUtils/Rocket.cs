using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rocket
{
    public string rocket_name;
    public SeconStage second_stage;
}

[System.Serializable]
public class SeconStage
{
    public Payloads[] payloads;
}

[System.Serializable]
public class Payloads
{
    public string nationality;
}