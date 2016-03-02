using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StrategyGame;
using System;

public abstract class DataManager<T> : ScriptableObject where T : DataType{

    private static List<T> savedData;
    public static List<T> SavedData { 
        set { savedData = value; } 
        get { 
            if (savedData != null) {
//                Debug.Log ("Data exists");
                return savedData;
            } else {
                Debug.Log ("Creating new List");
                savedData = new List<T> ();
                return savedData;
            }
        }
    }

    public GameDatabase myDatabase;

    public static void AddData() {
        savedData.Add (ScriptableObject.CreateInstance<T>());
    }

    public static void AddData(DataType dataType) {
        myDatabase.AddDataOfType (dataType);
    }

    public static void DeleteData(int id) {
        savedData.RemoveAt (GetIdIndex(id));
        savedData.TrimExcess ();
    }

    public static int GetIdIndex (int id) {
        return id;
    }

    public static void SaveSelectedData (int id, T data) {
        savedData [GetIdIndex(id)] = data;
    }
}
