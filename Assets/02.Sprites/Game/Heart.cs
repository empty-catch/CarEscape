using System;
using UnityEngine;

public class Heart : MonoBehaviour, IGridObject
{
    public static int Count { get; private set; }

    public void Execute(Action action)
    {
        Count--;
    }
}
