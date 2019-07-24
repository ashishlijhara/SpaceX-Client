using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ListItem<T>
{
    void Init(T item);
}
