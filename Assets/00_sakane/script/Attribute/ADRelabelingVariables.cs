using UnityEngine;
using UnityEditor;

// ラベルのみを変更するクラス
[CustomPropertyDrawer(typeof(ARelabelingVariables))]
public class ADRelabelingVariables : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		ARelabelingVariables relabelingVariables = attribute as ARelabelingVariables;
		EditorGUI.PropertyField(position, property, new GUIContent(relabelingVariables.label));
	}
}