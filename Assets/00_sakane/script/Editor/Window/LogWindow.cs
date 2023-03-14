using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class LogWindow : EditorWindow
{
	Dictionary<string, string> log;

    [MenuItem("Window/Log")]
    public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(LogWindow));
	}

	private void OnGUI()
	{
		// �h���b�v�_�E��
		if (GUILayout.Button("test", GUILayout.Width(50)))
		{
			// �h���b�v�_�E���쐬
			var dropdown = new MyDropDown(new AdvancedDropdownState());
			var buttonLabel = new GUIContent("Show");
			var buttonStyle = EditorStyles.toolbarButton;
			var buttonRect = GUILayoutUtility.GetRect(buttonLabel, buttonStyle);
			// �h���b�v�_�E���\��
			dropdown.Show(buttonRect);
		}

		// ��
		var splitterRect = EditorGUILayout.GetControlRect(false, GUILayout.Height(1));
		splitterRect.x = 0;
		splitterRect.width = position.width;
		// ���\��
		EditorGUI.DrawRect(splitterRect, Color.black);

		// ���O�\��
		//foreach(var v in log)
		//{
			//GUILayout.TextArea(v,);
		//}
	}
}
