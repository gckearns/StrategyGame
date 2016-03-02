using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.UI;
using System;

//[CustomEditor(typeof(BuildingType))]
public class BuildingEditorWindow : EditorWindow {

    [MenuItem ("Window/Building Editor")]
    public static void  ShowWindow () {
        EditorWindow.GetWindow(typeof(BuildingEditorWindow));
    }

    private Vector2 scrollPosition;
    private Vector2 scrollPosition2;
    private int bldgSelection;
    private GUIStyle customStyle;
    private BuildingType bldg;
    private BuildingType[] bldgTypes;
    private string[] strings;
    private string[] ids;
    private string[] cats;
    private bool initialized = false;

    void Update () {

    }

    void OnGUI () {
        if (!initialized) {
            Initialize ();
        }
        bool bldgListPopulated = false;
        RefreshBuildingData ();
//        if (bldgTypes.Length > 0) {
//            bldgListPopulated = true;
//        }
        GUILayoutOption xWidth = GUILayout.ExpandWidth (true);
        GUILayoutOption xWidthNo = GUILayout.ExpandWidth (false);

        // Split window into two panes
        EditorGUILayout.BeginHorizontal();

        // Left pane
        scrollPosition = EditorGUILayout.BeginScrollView (scrollPosition);
        EditorGUILayout.BeginHorizontal (xWidthNo);

        EditorGUI.BeginChangeCheck ();
        // Name column
        EditorGUILayout.BeginVertical (xWidthNo);
        GUI.SetNextControlName ("name");
        if (GUILayout.Button ("Building Name")) {
            //sort by name
        }
//        if (bldgListPopulated) {
            bldgSelection = GUILayout.SelectionGrid (bldgSelection, strings, 1, customStyle, xWidth);
//        }
        EditorGUILayout.EndVertical ();

        // ID column
        EditorGUILayout.BeginVertical (xWidthNo);
        if (GUILayout.Button ("ID")) {
            //sort by ID
        }
//        if (bldgListPopulated) {
            bldgSelection = GUILayout.SelectionGrid (bldgSelection, ids, 1, customStyle, xWidth);
//        }
        EditorGUILayout.EndVertical ();

        // Category column
        EditorGUILayout.BeginVertical (xWidthNo);
        if (GUILayout.Button ("Category")) {
            //sort by category
        }
//        if (bldgListPopulated) {
            bldgSelection = GUILayout.SelectionGrid (bldgSelection, cats, 1, customStyle, xWidth);
//        }
        EditorGUILayout.EndVertical ();

        bool changed = EditorGUI.EndChangeCheck ();
        if (changed) {
            GUI.FocusControl ("name");
        }
//        if (bldgListPopulated) {
            if (bldgSelection < 0 || bldgSelection >= bldgTypes.Length) {
                bldgSelection = 0;
            }
        if (bldgTypes.Length > 0) {
            bldg = bldgTypes [bldgSelection];
        }
//        }
        EditorGUILayout.EndHorizontal ();
        EditorGUILayout.EndScrollView ();

        // Right pane
        scrollPosition2 = EditorGUILayout.BeginScrollView (scrollPosition2,xWidth);
        EditorGUILayout.BeginVertical ();
        if (bldgTypes.Length > 0) {
            bldg.dataName = EditorGUILayout.TextField ("Name", bldg.dataName);
            bldg.description = EditorGUILayout.TextField ("Description", bldg.description);
            bldg.id = EditorGUILayout.IntField ("ID", bldg.id);
            bldg.category = (StrategyGame.BuildingCategory)EditorGUILayout.EnumPopup ("Category", bldg.category);
            bldg.size = EditorGUILayout.IntSlider ("Size", bldg.size, 1, 3);
            bldg.powerRequirement = EditorGUILayout.IntField ("Power", bldg.powerRequirement);

            string[] itemTypes = new string[ItemManager.SavedData.Count];
            for (int i = 0; i < ItemManager.SavedData.Count; i++) {
//            bldg.costTypes[i] = EditorGUILayout.ObjectField ("Item Type", bldg.costTypes[i], typeof(InventoryItem), 
//                true) as InventoryItem;
                itemTypes [i] = ItemManager.SavedData[i].dataName;
            }
            for (int i = 0; i < bldg.costTypes.Count; i++) {
                int selectedItem = bldg.costTypes [i].id;
                selectedItem = EditorGUILayout.Popup (selectedItem, itemTypes);
                bldg.costTypes [i] = ItemManager.SavedData [selectedItem];
            }

            EditorGUILayout.BeginHorizontal (); //Button Group for item types
            if (GUILayout.Button ("Add Cost Item")) {
                BuildingManager.AddCostType (bldgSelection);
            }
            int itemSelection = 0;
            if (GUILayout.Button ("Delete Cost Item")) {
                BuildingManager.DeleteCostType (bldgSelection, itemSelection);
            }
            EditorGUILayout.EndHorizontal (); //Button Group for item types
            bldg.icon = (Sprite)EditorGUILayout.ObjectField ("Icon", bldg.icon, typeof(Sprite), true);
        }
        EditorGUILayout.EndVertical ();
        EditorGUILayout.EndScrollView ();
        EditorGUILayout.EndHorizontal ();
        if (bldgTypes.Length > 0) {
            BuildingManager.SavedData [bldgSelection] = bldg;
        }
        EditorGUILayout.BeginHorizontal ();
        GUI.SetNextControlName ("Add");
        if (GUILayout.Button ("Add")) {
            BuildingManager.AddData ();
            GUI.FocusControl ("Add");
        }
        GUI.SetNextControlName ("Delete");
        if (GUILayout.Button ("Delete")) {
            BuildingManager.DeleteData (bldgSelection);
            if (bldgListPopulated) {
                bldgSelection--;
            } else {
                bldgSelection = 0;
            }
            GUI.FocusControl ("Delete");
        }
        GUI.SetNextControlName ("Refresh");
        if (GUILayout.Button ("Refresh")) {
            LoadBuilding();
            GUI.FocusControl ("Refresh");
        }
        EditorGUILayout.EndHorizontal ();
    }

    void LoadBuilding () {
        BuildingType loadedBldg = AssetDatabase.LoadAssetAtPath<BuildingType> ("Assets/Scripts/HouseBldg.asset");
        BuildingManager.SavedData.Add (loadedBldg);
    }

    void RefreshBuildingData () {
        bldgTypes = BuildingManager.SavedData.ToArray ();
        strings = new string[bldgTypes.Length];
        ids = new string[bldgTypes.Length];
        cats = new string[bldgTypes.Length];
        for (int i = 0; i < bldgTypes.Length; i++) {
            strings [i] = bldgTypes [i].dataName;
            ids [i] = bldgTypes [i].id.ToString();
            cats [i] = bldgTypes [i].category.ToString();
        }
    }

    void Initialize () {
        customStyle = GUI.skin.box;
        customStyle.hover = GUI.skin.button.hover;
        customStyle.active = GUI.skin.button.active;
        customStyle.onNormal = GUI.skin.button.onNormal; 
        customStyle.onHover = GUI.skin.button.onHover;
        customStyle.onActive = GUI.skin.button.onActive;
        Debug.Log ("Editor UI initialized.");
        initialized = true;
    }
}
