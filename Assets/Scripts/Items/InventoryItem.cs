using UnityEngine;
using System.Collections;
using System;
using StrategyGame;

[Serializable]
[CreateAssetMenu]
public class InventoryItem : ScriptableObject, IComparable<InventoryItem>{

    public string itemName;
    public int id;
    public ItemCategory category;

    public int CompareTo (InventoryItem otherItem) {
        if (otherItem == null) {
            return 1;
        } else
            return -1;
    }

    public override string ToString ()
    {
        return string.Format ("[InventoryItem] {0}", itemName);
    }
}
