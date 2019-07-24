using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpaceXJsonFactory<T> : ISpaceXJsonFactory<T>
{
    public MonoBehaviour refMb;
    bool isDone = false;
    bool isError = false;
    public bool IsDone { get => isDone; }

    public bool IsError { get => isError; }

    string output = null;

    public void FetchJson(string url)
    {
        if (!refMb)
            throw new Exception("Referencing Monobehaviour not assigned");
        isDone = false; isError = false;
        refMb.StartCoroutine(Fetch(url));
    }

    IEnumerator Fetch(string url)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log("Error!");
            isError = true;
        }
        else
            output = unityWebRequest.downloadHandler.text;
        isDone = true;
    }
    public virtual string GetJson()
    {
        return output;
    }

    public virtual T GetRootObject()
    {
        return JsonUtility.FromJson<T>(GetJson());
    }

    public void SetMonoBehaviour(MonoBehaviour mb)
    {
        refMb = mb;
    }
}
