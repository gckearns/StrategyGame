using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[ExecuteInEditMode]
public class ItemManager : MonoBehaviour{
    
    public List<InventoryItem> SavedItems;

    private static List<InventoryItem> cachedItems;
    private static bool cacheChanged = false;
    private static int selectedItemId;

    void OnGUI() {
        UpdateTypes ();
    }

    public static void AddItem() {
        cachedItems.Add (ScriptableObject.CreateInstance<InventoryItem>());
        cacheChanged = true;
    }

    public static void DeleteItem(int i) {
        cachedItems.RemoveAt (i);
        cachedItems.TrimExcess ();
        cacheChanged = true;
    }

    public void UpdateTypes() {
        // cache stored inventory item data for list use
        if (cacheChanged) {
            SavedItems = cachedItems;
            cacheChanged = false;
        }
        cachedItems = SavedItems;
    }

    public static InventoryItem[] GetCachedItems() {
        return cachedItems.ToArray ();
    }

    public static void SaveSelectedItem(int i, InventoryItem item) {
        cachedItems [i] = item;
        cacheChanged = true;
    }
}
