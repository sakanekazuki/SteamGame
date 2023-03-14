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
		// ドロップダウン
		if (GUILayout.Button("test", GUILayout.Width(50)))
		{
			// ドロップダウン作成
			var dropdown = new MyDropDown(new AdvancedDropdownState());
			var buttonLabel = new GUIContent("Show");
			var buttonStyle = EditorStyles.toolbarButton;
			var buttonRect = GUILayoutUtility.GetRect(buttonLabel, buttonStyle);
			// ドロップダウン表示
			dropdown.Show(buttonRect);
		}

		// 線
		var splitterRect = EditorGUILayout.GetControlRect(false, GUILayout.Height(1));
		splitterRect.x = 0;
		splitterRect.width = position.width;
		// 線表示
		EditorGUI.DrawRect(splitterRect, Color.black);

		// ログ表示
		//foreach(var v in log)
		//{
			//GUILayout.TextArea(v,);
		//}
	}
}
