using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class DataTable : ScriptableObject
{
	public static void AdaptToDataTable<T>(List<T> tList) where T : new()
	{
		// データテーブルを空にする
		tList.Clear();
		// ファイル名
		var fileName = "";
		// ファイル取得
		fileName = EditorUtility.OpenFilePanel("CSVLoad", "", "");

		// Resourceより下のフォルダを取得
		fileName = fileName.Substring(fileName.LastIndexOf("Resources/"));
		// 拡張子を抜く
		fileName = fileName.Substring(0, fileName.LastIndexOf("."));
		// Resourcesを抜く
		fileName = fileName.Substring(fileName.IndexOf("/") + 1);
		// ファイル読み込み
		var data = TextLoader.CSVLoad(fileName);

		// 変数名
		var propertyNames = new List<string>();

		foreach (var line in data)
		{
			// 行番号
			int lineNumber = data.IndexOf(line);

			T dataTable = new T();
			foreach (var value in line)
			{
				// 列
				int columnNumber = line.IndexOf(value);

				// 1行目
				if (lineNumber == 1)
				{
					propertyNames.Add(value);
					continue;
				}

				// 2行目以降

				// 変数の種類取得
				var propType = dataTable.GetType();
				// アクセスできる変数取得
				var propInfo = propType.GetProperty(propertyNames[columnNumber]);

				// 無いプロパティ名がある場合代入しない
				if (propInfo == null)
				{
					Debug.LogError("変数が存在しません");
					return;
				}

				if (typeof(int) == propInfo.PropertyType)
				{
					propInfo.SetValue(dataTable, int.Parse(data[lineNumber][columnNumber]));
				}
				else if (typeof(float) == propInfo.PropertyType)
				{
					propInfo.SetValue(dataTable, float.Parse(data[lineNumber][columnNumber]));
				}
				else if (typeof(string) == propInfo.PropertyType)
				{
					propInfo.SetValue(dataTable, data[lineNumber][columnNumber]);
				}
				else
				{
					Debug.LogError("入らない型です");
				}
			}
			if (lineNumber >= 2)
			{
				tList.Add(dataTable);
			}
		}
	}
}
