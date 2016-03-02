using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.UI;

//[CustomEditor(typeof(BuildingType))]
public class ItemEditorWindow : EditorWindow {

    [MenuItem ("Window/Item Editor")]
    public static void  ShowWindow () {
        EditorWindow.GetWindow(typeof(ItemEditorWindow));
    }

    private Vector2 scrollPosition;
    private Vector2 scrollPosition2;
    private int selection;
    private GUIStyle customStyle;
//    private ItemType item;
//    private ItemType[] itemTypes;
    private string[] strings;
    private string[] ids;
    private string[] cats;
    private bool initialized = false;

    void Callback(object obj)
    {
        Debug.Log("Selected: " + obj);
    }

    void OnGUI () {
        if (!initialized) {
            Initialize ();
        }
//        bool itemListPopulated = false;
//        RefreshItemData ();
//        if (itemTypes.Length > 0) {
//            itemListPopulated = true;
//        }
//        GUILayoutOption xWidth = GUILayout.ExpandWidth (true);
//        GUILayoutOption xWidthNo = GUILayout.ExpandWidth (false);
//
//        // Split window into two panes
//        EditorGUILayout.BeginHorizontal();
//
//        // Left pane
//        scrollPosition = EditorGUILayout.BeginScrollView (scrollPosition);
//        EditorGUILayout.BeginHorizontal (xWidthNo);
//
//        // Name column
//        EditorGUILayout.BeginVertical (xWidthNo);
//
//        if (GUILayout.Button ("Item Name")) {
//            //sort by name
//        }
//        if (itemListPopulated) {
//            selection = GUILayout.SelectionGrid (selection, strings, 1, customStyle, xWidth);
//        }
//        EditorGUILayout.EndVertical ();
//
//        // ID column
//        EditorGUILayout.BeginVertical (xWidthNo);
//        if (GUILayout.Button ("ID")) {
//            //sort by ID
//        }
//        if (itemListPopulated) {
//            selection = GUILayout.SelectionGrid (selection, ids, 1, customStyle, xWidth);
//        }
//        EditorGUILayout.EndVertical ();
//
//        // Category column
//        EditorGUILayout.BeginVertical (xWidthNo);
//        if (GUILayout.Button ("Category")) {
//            //sort by category
//        }
//        if (itemListPopulated) {
//            selection = GUILayout.SelectionGrid (selection, cats, 1, customStyle, xWidth);
//        }
//        EditorGUILayout.EndVertical ();
//        if (itemListPopulated) {
//            item = itemTypes [selection];
//        }
//
//        EditorGUILayout.EndHorizontal ();
//        EditorGUILayout.EndScrollView ();
//
//        // Right pane
//        scrollPosition2 = EditorGUILayout.BeginScrollView (scrollPosition2, xWidth);
//        EditorGUILayout.BeginVertical ();
//        if (itemListPopulated) {
//        item.itemName = EditorGUILayout.TextField ("Name", item.itemName);
//            item.id = EditorGUILayout.IntField ("ID", item.id);
//            item.category = (StrategyGame.ItemCategory)EditorGUILayout.EnumPopup ("Category", item.category);
//        }
//        EditorGUILayout.EndVertical ();
//        EditorGUILayout.EndScrollView ();
//        EditorGUILayout.EndHorizontal ();
//        if (itemListPopulated) {
//            ItemManager.SaveSelectedItem (selection, item);
//        }
//
//        EditorGUILayout.BeginHorizontal ();
//        GUI.SetNextControlName ("Add");
//        if (GUILayout.Button ("Add")) {
//            ItemManager.AddItem ();
//            GUI.FocusControl ("Add");
//        }
//        GUI.SetNextControlName ("Delete");
//        if (GUILayout.Button ("Delete")) {
//            ItemManager.DeleteItem (selection);
//            if (itemListPopulated) {
//                selection--;
//            } else {
//                selection = 0;
//            }
//            GUI.FocusControl ("Delete");
//        }
//        EditorGUILayout.EndHorizontal ();
    }

    void RefreshItemData () {
//        itemTypes = ItemManager.GetCachedItems();
//        strings = new string[itemTypes.Length];
//        ids = new string[itemTypes.Length];
//        cats = new string[itemTypes.Length];
//        for (int i = 0; i < itemTypes.Length; i++) {
//            strings [i] = itemTypes [i].itemName;
//            ids [i] = itemTypes [i].id.ToString();
//            cats [i] = itemTypes [i].category.ToString();
//        }
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
