using UnityEngine;
using System.Collections;
using StrategyResources;

public class UICanvas : MonoBehaviour {

	public void ShowContextMenu(Tile tile){
		if (tile.terrainType == TileTerrainType.Default){
			SetContextTerrainDefault(tile);
			gameObject.SetActive(true);
		}
	}

	private void SetContextTerrainDefault(Tile tile){
	}
}
