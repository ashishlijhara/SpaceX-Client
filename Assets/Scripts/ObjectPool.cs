using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectPrefab;
    Stack<GameObject> inactiveObjects = new Stack<GameObject>();

    public GameObject RequestAnObject()
    {
        GameObject requestedObject = null;

        if (inactiveObjects.Count > 0)
            requestedObject = inactiveObjects.Pop();
        else
        {
            requestedObject = Instantiate(objectPrefab) as GameObject;
            Pool pooledObject = requestedObject.AddComponent<Pool>();
            pooledObject.pool = this;
        }
        requestedObject.transform.SetParent(null);
        requestedObject.SetActive(true);
        return requestedObject;
    }

    public void StoreInPool(GameObject objToStore)
    {
        Pool obj = objToStore.GetComponent<Pool>();
        if (obj!=null && obj.pool == this)
        {
            objToStore.transform.SetParent(transform);
            objToStore.SetActive(false);
            inactiveObjects.Push(objToStore);
        }
    }
}

public class Pool: MonoBehaviour
{
    public ObjectPool pool;
}
