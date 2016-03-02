using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StrategyGame;

public class BuildingManager : DataManager<BuildingType>{

    public static void AddCostType(int bldg) {
        SavedData[bldg].costTypes.Add (ItemManager.SavedData[0]);
    }

    public static void DeleteCostType(int bldg, int i) {
        SavedData[bldg].costTypes.RemoveAt (i);
        SavedData[bldg].costTypes.TrimExcess ();
    }

    public static BuildingType[] GetBuildingsOfCategory(int category) {
        List<BuildingType> buildings = new List<BuildingType>();
        for (int i = 0; i < SavedData.Count; i++) {
            if (SavedData [i].category == GameResources.BuildingCategories[category]) {
                buildings.Add (SavedData[i]);
            }
        }
        return buildings.ToArray ();
    }
}
