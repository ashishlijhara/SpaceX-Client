using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedLenQueue<T> : Queue<T>
{
    int capacity;

    public int MaxCapacity { get => capacity; set => capacity = value; }

    public FixedLenQueue(int cap){ capacity = cap;}

    public void Add(T t)
    {
        if (Count == MaxCapacity)
            Dequeue();
        Enqueue(t);
    }
}
