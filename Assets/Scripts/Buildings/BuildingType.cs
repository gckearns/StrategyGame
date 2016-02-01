using UnityEngine;
using System.Collections;
using System;
using StrategyGame;
using System.Collections.Generic;

[Serializable]
[CreateAssetMenu]
public class BuildingType : ScriptableObject, IComparable<BuildingType>{
    public string buildingType = "Default";
    public int id = BuildingManager.GetCachedBuildings().Length;
    public BuildingCategory category = BuildingCategory.Housing;
    public Sprite icon;
    public string description = "No description.";
    public List<InventoryItem> costTypes = new List<InventoryItem>();
    public int[] costAmounts;
    public int powerRequirement = 0;
    public int size = 1;
    public int population = 0;
    public List<InventoryItem> yieldTypes = new List<InventoryItem>();
    public float[] yieldAmounts;
    public float yieldFrequency;

    public int CompareTo (BuildingType otherBldg) {
        if (otherBldg == null) {
            return 1;
        } else
            return -1;
    }
}
