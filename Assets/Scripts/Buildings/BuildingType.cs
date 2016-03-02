using UnityEngine;
using System.Collections;
using StrategyGame;
using System.Collections.Generic;
using System;

[Serializable]
[CreateAssetMenu]
public class BuildingType : DataType{
    public BuildingCategory category = BuildingCategory.Housing;
    public Sprite icon;
    public string description = "No description.";
    public List<ItemType> costTypes = new List<ItemType>();
    public int[] costAmounts;
    public int powerRequirement = 0;
    public int size = 1;
    public int population = 0;
    public List<ItemType> yieldTypes = new List<ItemType>();
    public float[] yieldAmounts;
    public float yieldFrequency;
}
