using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

// Statusをまとめて格納できるScriptableObject
[CreateAssetMenu(fileName = "Data",menuName = "ScriptableObjects/Status")]
public class StatusDataTable : DataTable
{
	// Statusをまとめて格納する配列
	public List<BaseStatus> statuses = new List<BaseStatus>(10);

	// CSVの読み込みを行うメニュー
	[ContextMenu("CSVLoad")]
	public void CSVLoading()
	{
		AdaptToDataTable<BaseStatus>(statuses);

		//// ステータスを空にする
		//statuses.Clear();
		//// CSVデータ取得
		//var data = TextLoader.CSVLoad("CSV/test");
		//// 変数名
		//var propertyNames = new List<string>();

		//foreach (var line in data)
		//{
		//	// 行
		//	int lineNumber = data.IndexOf(line);

		//	// 読み込んだデータ
		//	var status = new BaseStatus();
		//	foreach (var value in line)
		//	{
		//		// 列
		//		int columnNumber = line.IndexOf(value);

		//		// 1行目
		//		if (lineNumber == 1)
		//		{
		//			propertyNames.Add(value);
		//			continue;
		//		}

		//		// 2行目以降

		//		// 変数の種類取得
		//		var propType = status.GetType();
		//		// アクセスできる変数取得
		//		var propInfo = propType.GetProperty(propertyNames[columnNumber]);

		//		// 無いプロパティ名がある場合代入しない
		//		if (propInfo == null)
		//		{
		//			Debug.LogError("変数が存在しません");
		//			return;
		//		}

		//		if (typeof(int) == propInfo.PropertyType)
		//		{
		//			propInfo.SetValue(status, int.Parse(data[lineNumber][columnNumber]));
		//		}
		//		else if (typeof(float) == propInfo.PropertyType)
		//		{
		//			propInfo.SetValue(status, float.Parse(data[lineNumber][columnNumber]));
		//		}
		//		else if (typeof(string) == propInfo.PropertyType)
		//		{
		//			propInfo.SetValue(status, data[lineNumber][columnNumber]);
		//		}
		//		else
		//		{
		//			Debug.LogError("入らない型です");
		//		}
		//	}
		//	if (lineNumber >= 2)
		//	{
		//		statuses.Add(status);
		//	}
		//}
	}
}