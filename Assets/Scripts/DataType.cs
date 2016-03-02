using UnityEngine;
using System.Collections;
using System;
using StrategyGame;
using System.Collections.Generic;

public abstract class DataType : ScriptableObject, IComparable<DataType>{
    public string dataName = "Default";
    public int id;

    public int CompareTo (DataType otherData) {
        if (otherData == null) {
            return 1;
        } else
            return -1;
    }
}