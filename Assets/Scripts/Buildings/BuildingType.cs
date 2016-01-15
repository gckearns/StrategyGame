using UnityEngine;
using System.Collections;
using StrategyGame;

public class BuildingType : MonoBehaviour {
    public string buildingType;
    public Sprite icon;
    public string description;
    public InventoryItem[] costTypes;
    public int[] costAmounts;
    public int size;
    public int population;
    public InventoryItem[] yieldTypes;
    public float yieldAmounts;
    public float yieldFrequency;
}