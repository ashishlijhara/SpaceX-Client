using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISpaceXJsonFactory<T>
{
    void SetMonoBehaviour(MonoBehaviour mb);
    bool IsDone { get; }
    bool IsError { get; }
    void FetchJson(string url);
    string GetJson();
    T GetRootObject();
}
