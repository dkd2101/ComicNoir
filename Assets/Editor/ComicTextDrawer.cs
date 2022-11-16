using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ComicText))]
    public class ComicTextDrawer : PropertyDrawer
    {
        private const float Spacing = 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var typeProp = property.FindPropertyRelative("type");
            var panelType = (ComicPanelType)typeProp.enumValueIndex;

            var yPos = position.y + Spacing;

            var typeRect = new Rect(position.x, yPos, position.width, EditorGUI.GetPropertyHeight(typeProp));
            typeProp.enumValueIndex = (int)(object) EditorGUI.EnumPopup(typeRect, typeProp.displayName, panelType);
            yPos += EditorGUI.GetPropertyHeight(typeProp) + Spacing * 2;

            var textProp = property.FindPropertyRelative("text");
            var textRect = new Rect(position.x, yPos, position.width, EditorGUI.GetPropertyHeight(textProp));
            textProp.stringValue = EditorGUI.DelayedTextField(textRect, textProp.stringValue);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var typeProp = property.FindPropertyRelative("type");
            var textsProp = property.FindPropertyRelative("texts");
            
            return Spacing * 4 +
                   //EditorGUI.GetPropertyHeight(typeProp) +
                   EditorGUI.GetPropertyHeight(textsProp);
        }
    }
}
