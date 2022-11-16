using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ComicPanel))]
    public class ComicPanelDrawer : PropertyDrawer
    {
        private const float Spacing = 2f;

        private readonly Dictionary<string, bool> _foldout = new Dictionary<string, bool>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var typeProp = property.FindPropertyRelative("type");
            var panelType = (ComicPanelType)typeProp.enumValueIndex;

            var yPos = position.y + Spacing;
            
            var dropdownRect = new Rect(position.x + 10, yPos, 20, EditorGUI.GetPropertyHeight(typeProp));
            
            var typeRect = new Rect(position.x + 20, yPos, position.width - 20, EditorGUI.GetPropertyHeight(typeProp));
            typeProp.enumValueIndex = (int)(object) EditorGUI.EnumPopup(typeRect, typeProp.displayName, panelType);
            yPos += EditorGUI.GetPropertyHeight(typeProp) + Spacing * 2;

            var key = property.propertyPath;
            var foldout = _foldout.GetValueOrDefault(key, false);
            
            var newFoldout = EditorGUI.Foldout(dropdownRect, foldout, GUIContent.none);
            if (foldout != newFoldout)
                if (_foldout.ContainsKey(key))
                    _foldout[key] = newFoldout;
                else
                    _foldout.Add(key, newFoldout);

            if (!newFoldout) return;

            switch (panelType)
            {
                case ComicPanelType.Image:
                    // var alignProp = property.FindPropertyRelative("alignment");
                    // var alignRect = new Rect(position.x, yPos, position.width, EditorGUI.GetPropertyHeight(alignProp));
                    // alignProp.enumValueIndex = (int)(object) EditorGUI.EnumPopup(alignRect, alignProp.displayName, (AlignImage)alignProp.enumValueIndex);
                    // yPos += EditorGUI.GetPropertyHeight(alignProp) + Spacing * 2;
                    
                    var imageProp = property.FindPropertyRelative("image");
                    var imageRect = new Rect(position.x, yPos, position.width, EditorGUI.GetPropertyHeight(imageProp));
                    imageProp.objectReferenceValue = EditorGUI.ObjectField(imageRect, imageProp.displayName, imageProp.objectReferenceValue, typeof(ComicPanel), false);
                    yPos += EditorGUI.GetPropertyHeight(imageProp) + Spacing * 2;
                    
                    var frameProp = property.FindPropertyRelative("frame");
                    var frameRect = new Rect(position.x, yPos, position.width, EditorGUI.GetPropertyHeight(frameProp));
                    frameProp.enumValueIndex = (int)(object) EditorGUI.EnumPopup(frameRect, frameProp.displayName, (ImageFrame)frameProp.enumValueIndex);
                    yPos += EditorGUI.GetPropertyHeight(frameProp) + Spacing * 2;
                    
                    var textsProp = property.FindPropertyRelative("texts");
                    var textsRect = new Rect(position.x, yPos, position.width, EditorGUI.GetPropertyHeight(textsProp));
                    EditorGUI.PropertyField(textsRect, textsProp);
                    yPos += EditorGUI.GetPropertyHeight(textsProp) + Spacing * 2;
                    break;
                
                case ComicPanelType.Monologue:
                    var textProp = property.FindPropertyRelative("monologue");
                    var textRect = new Rect(position.x, yPos, position.width, EditorGUI.GetPropertyHeight(textProp));
                    textProp.stringValue = EditorGUI.TextArea(textRect, textProp.stringValue);
                    yPos += EditorGUI.GetPropertyHeight(textProp) + Spacing * 2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var typeProperty = property.FindPropertyRelative("type");
            var panelType = (ComicPanelType)typeProperty.enumValueIndex;
            var height = EditorGUI.GetPropertyHeight(typeProperty);

            if (!_foldout.GetValueOrDefault(property.propertyPath, false)) return height + Spacing * 2;
            
            switch (panelType)
            {
                case ComicPanelType.Image:
                    // var alignProp = property.FindPropertyRelative("alignment");
                    var imageProp = property.FindPropertyRelative("image");
                    var frameProp = property.FindPropertyRelative("frame");
                    var textsProp = property.FindPropertyRelative("texts");
                    return height + /* EditorGUI.GetPropertyHeight(alignProp) +*/ EditorGUI.GetPropertyHeight(imageProp) + EditorGUI.GetPropertyHeight(frameProp) +
                           EditorGUI.GetPropertyHeight(textsProp) + Spacing * 8;
                case ComicPanelType.Monologue:
                    var textProp = property.FindPropertyRelative("monologue");
                    return height + EditorGUI.GetPropertyHeight(textProp) + Spacing * 4;
                default:
                    return base.GetPropertyHeight(property, label);
            }
        }
    }
}