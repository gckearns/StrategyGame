using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using StrategyGame;

[ExecuteInEditMode]
public class BuildingManager : MonoBehaviour{

    public List<BuildingType> SavedBuildings;

    private static List<BuildingType> cachedBuildings;
    private static bool cacheChanged = false;
    private static int selectedItemId;

    void OnGUI() {
        UpdateTypes ();
    }

    public static void AddBuilding() {
        cachedBuildings.Add (ScriptableObject.CreateInstance<BuildingType>());
        cacheChanged = true;
    }

    public static void DeleteBuilding(int i) {
        cachedBuildings.RemoveAt (i);
        cachedBuildings.TrimExcess ();
        cacheChanged = true;
    }

    public static void AddCostType(int bldg) {
        cachedBuildings[bldg].costTypes.Add (ItemManager.GetCachedItems()[0]);
        cacheChanged = true;
    }

    public static void DeleteCostType(int bldg, int i) {
        cachedBuildings[bldg].costTypes.RemoveAt (i);
        cachedBuildings[bldg].costTypes.TrimExcess ();
        cacheChanged = true;
    }

    public void UpdateTypes() {
        // cache stored building data for list use
        if (cacheChanged) {
            SavedBuildings = cachedBuildings;
            cacheChanged = false;
            print ("Saved cache to inspector");
        } else {

        }
        print ("Saved inspector to cache");
        cachedBuildings = SavedBuildings;
    }

    public static BuildingType[] GetCachedBuildings() {
        return cachedBuildings.ToArray ();
    }

    public static BuildingType[] GetBuildingsOfCategory(int category) {
        List<BuildingType> buildings = new List<BuildingType>();
        for (int i = 0; i < cachedBuildings.Count; i++) {
            if (cachedBuildings [i].category == GameResources.BuildingCategories[category]) {
                buildings.Add (cachedBuildings [i]);
            }
        }
        return buildings.ToArray ();
    }

    public static void SaveSelectedBuilding(int i, BuildingType building) {
        cachedBuildings [i] = building;
        cacheChanged = true;
    }

    public static void CheckId () {
        
    }
}
