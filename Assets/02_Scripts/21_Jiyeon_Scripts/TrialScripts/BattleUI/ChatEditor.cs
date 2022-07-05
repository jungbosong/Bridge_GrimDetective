using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BattleManager))]
public class ChatEditor : Editor
{
    /*
    BattleManager battleManager;
    string text;

    void OnEnable()
    {
        battleManager = target as BattleManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        text = EditorGUILayout.TextArea(text);

        if(GUILayout.Button("보내기", GUILayout.Width(60)) && text.Trim() != "")
        {
            battleManager.Chat(true, text, null);
            text = "";
            GUI.FocusControl(null);
        }
        
        if(GUILayout.Button("받기", GUILayout.Width(60)) && text.Trim() != "")
        {
            battleManager.Chat(false, text, null);
            text = "";
            GUI.FocusControl(null);
        }

        EditorGUILayout.EndHorizontal();
    }
    */
}
