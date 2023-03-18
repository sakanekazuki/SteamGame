using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

// Status���܂Ƃ߂Ċi�[�ł���ScriptableObject
[CreateAssetMenu(fileName = "Data",menuName = "ScriptableObjects/Status")]
public class StatusDataTable : DataTable
{
	// Status���܂Ƃ߂Ċi�[����z��
	public List<BaseStatus> statuses = new List<BaseStatus>(10);

	// CSV�̓ǂݍ��݂��s�����j���[
	[ContextMenu("CSVLoad")]
	public void CSVLoading()
	{
		AdaptToDataTable<BaseStatus>(statuses);

		//// �X�e�[�^�X����ɂ���
		//statuses.Clear();
		//// CSV�f�[�^�擾
		//var data = TextLoader.CSVLoad("CSV/test");
		//// �ϐ���
		//var propertyNames = new List<string>();

		//foreach (var line in data)
		//{
		//	// �s
		//	int lineNumber = data.IndexOf(line);

		//	// �ǂݍ��񂾃f�[�^
		//	var status = new BaseStatus();
		//	foreach (var value in line)
		//	{
		//		// ��
		//		int columnNumber = line.IndexOf(value);

		//		// 1�s��
		//		if (lineNumber == 1)
		//		{
		//			propertyNames.Add(value);
		//			continue;
		//		}

		//		// 2�s�ڈȍ~

		//		// �ϐ��̎�ގ擾
		//		var propType = status.GetType();
		//		// �A�N�Z�X�ł���ϐ��擾
		//		var propInfo = propType.GetProperty(propertyNames[columnNumber]);

		//		// �����v���p�e�B��������ꍇ������Ȃ�
		//		if (propInfo == null)
		//		{
		//			Debug.LogError("�ϐ������݂��܂���");
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
		//			Debug.LogError("����Ȃ��^�ł�");
		//		}
		//	}
		//	if (lineNumber >= 2)
		//	{
		//		statuses.Add(status);
		//	}
		//}
	}
}