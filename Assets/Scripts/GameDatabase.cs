using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
[CreateAssetMenu]
public class GameDatabase : ScriptableObject {

//    public List<List<DataType>> lists;

    public void AddDataOfType (DataType dataType){
        Type t = dataType.GetType ();
        ScriptableObject.CreateInstance (t);
    }
}
