using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class DataTable : ScriptableObject
{
	public static void AdaptToDataTable<T>(List<T> tList) where T : new()
	{
		// �f�[�^�e�[�u������ɂ���
		tList.Clear();
		// �t�@�C����
		var fileName = "";
		// �t�@�C���擾
		fileName = EditorUtility.OpenFilePanel("CSVLoad", "", "");

		// Resource��艺�̃t�H���_���擾
		fileName = fileName.Substring(fileName.LastIndexOf("Resources/"));
		// �g���q�𔲂�
		fileName = fileName.Substring(0, fileName.LastIndexOf("."));
		// Resources�𔲂�
		fileName = fileName.Substring(fileName.IndexOf("/") + 1);
		// �t�@�C���ǂݍ���
		var data = TextLoader.CSVLoad(fileName);

		// �ϐ���
		var propertyNames = new List<string>();

		foreach (var line in data)
		{
			// �s�ԍ�
			int lineNumber = data.IndexOf(line);

			T dataTable = new T();
			foreach (var value in line)
			{
				// ��
				int columnNumber = line.IndexOf(value);

				// 1�s��
				if (lineNumber == 1)
				{
					propertyNames.Add(value);
					continue;
				}

				// 2�s�ڈȍ~

				// �ϐ��̎�ގ擾
				var propType = dataTable.GetType();
				// �A�N�Z�X�ł���ϐ��擾
				var propInfo = propType.GetProperty(propertyNames[columnNumber]);

				// �����v���p�e�B��������ꍇ������Ȃ�
				if (propInfo == null)
				{
					Debug.LogError("�ϐ������݂��܂���");
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
					Debug.LogError("����Ȃ��^�ł�");
				}
			}
			if (lineNumber >= 2)
			{
				tList.Add(dataTable);
			}
		}
	}
}
