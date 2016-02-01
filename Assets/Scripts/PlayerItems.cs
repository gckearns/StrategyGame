using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StrategyGame;

public class PlayerItems : MonoBehaviour {

    public InventoryItem[] items;
    public Dictionary<InventoryItem,int> itemDictionary;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        items = ItemManager.GetCachedItems ();
        for (int i = 0; i < items.Length; i++) {
            itemDictionary.Add (items [i], 0);
        }
        int val;
        itemDictionary.TryGetValue (items[0], out val);
        print ("player has" + val + items[0].itemName);
	}
}
