// using System;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
//
// [CustomEditor(typeof(ComicStrip))]
// public class ComicPanelEditor : Editor
// {
//     private ComicStrip _strip;
//     private SerializedProperty _panels;
//     
//     private void OnEnable()
//     {
//         _strip = target as ComicStrip;
//         
//         _panels = serializedObject.FindProperty("panels");
//     }
//
//     public override void OnInspectorGUI()
//     {
//         serializedObject.UpdateIfRequiredOrScript();
//         
//         _panels = serializedObject.FindProperty("panels");
//
//         EditorGUILayout.PropertyField(_panels, true);
//         _panels.NextVisible(true);
//         
//         base.OnInspectorGUI();
//     }
// }