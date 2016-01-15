using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class TileManager : MonoBehaviour{

    private Tile[,] tiles;
    private TileMap tileMap;
    private WorldController worldController;
    private Tile selectedTile;

    public TileManager Initialize(WorldController worldController, TileMap tileMap, Tile[,] tiles){
        this.worldController = worldController;
        this.tileMap = tileMap;
        this.tiles = tiles;
        return this;
    }

    public void SelectTile (Tile tile) {
        worldController.ClearTileHighlight ();
        worldController.TileHighlight (tile);
        selectedTile = tile;
        ModalPanel modalPanel = ModalPanel.Instance ();
        UnityAction buildAction = new UnityAction (OnBuildClicked);
        UnityAction cancelAction = new UnityAction (OnBuildClicked);
        modalPanel.BuildDialogue (GetDialogueInfo ().GetStrings (), modalPanel.iconImage.sprite, buildAction, cancelAction);
    }

    public void OnBuildClicked() {
        GameObject building = GameObject.Find("Building");
        building.transform.position = new Vector3 (selectedTile.worldCoords.x, 5f, selectedTile.worldCoords.z);
    }

    public DialogueTextArray GetDialogueInfo() {
        return new DialogueTextArray ("MyBuilding", "My building description.", "Size: 1x1    Pwr: 99999    Jobs: 99",
            "Yield time: 4 days", "Yields: $9999, 55 pancakes, 2 birds, 1 bush", 
            "Requires: 300 metal, 1 container, 10 mooncrete", "Have: (10k metal), (1 container), (10 mooncrete)");
    }
}
