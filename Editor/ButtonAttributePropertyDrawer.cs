using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NDY.Attribute
{
    [CustomPropertyDrawer(typeof(ButtonAttribute), true)]
    public class ButtonAttributePropertyDrawer : PropertyDrawer
    {
        private readonly float DEFAULT_HEIGHT = EditorGUIUtility.singleLineHeight * 1.5f;
        private readonly float DEFAULT_WEIGHT_FACTOR = .8f;
        private readonly float DEFAULT_SPACING = 2f;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var buttonAttribute = (ButtonAttribute)attribute;
            for (int i = 0; i < buttonAttribute.Methods.Length; i++)
            {
                Rect buttonRect = new Rect(position.x + ((1f - DEFAULT_WEIGHT_FACTOR) * .5f * position.width),
                    i * (DEFAULT_HEIGHT + DEFAULT_SPACING) + position.y,
                    position.width * DEFAULT_WEIGHT_FACTOR,
                    DEFAULT_HEIGHT);
                if (GUI.Button(buttonRect, buttonAttribute.Methods[i]))
                {
                    Object targetObject = property.serializedObject.targetObject;
                    var method = targetObject.GetType().GetMethod(buttonAttribute.Methods[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                    if (method == null)
                    {
                        Debug.LogError($"Could not find {buttonAttribute.Methods[i]} on {targetObject}");
                    }
                    else
                    {
                        method.Invoke(targetObject, null);
                    }
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var buttonAttribute = (ButtonAttribute)attribute;

            return buttonAttribute.Methods.Length * (DEFAULT_HEIGHT + DEFAULT_SPACING);
        }
    }
}
