using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

// Status���܂Ƃ߂Ċi�[�ł���ScriptableObject
[CreateAssetMenu(fileName = "Data",menuName = "ScriptableObjects/Status")]
public class StatusDataTable : ScriptableObject
{
	// Status���܂Ƃ߂Ċi�[����z��
	public List<BaseStatus> statuses = new List<BaseStatus>(10);

	// CSV�̓ǂݍ��݂��s�����j���[
	[ContextMenu("CSVLoad")]
	public void CSVLoading()
	{
		// �X�e�[�^�X����ɂ���
		statuses.Clear();
		// CSV�f�[�^�擾
		var data = TextLoader.CSVLoad("CSV/test");
		// �ϐ���
		var propertyNames = new List<string>();

		// �Ƃ肠����������Ŕėp�������߂�
		var status = new BaseStatus();
		foreach(var line in data)
		{
			if(data.IndexOf(line) <= 1)
			{
				continue;
			}
			status.name = line[0];
			status.HP = int.Parse(line[1]);
			status.Attack = int.Parse(line[2]);
			status.Defence = int.Parse(line[3]);
			statuses.Add(status);
		}
		

		//foreach(var line in data)
		//{
		//	// �s
		//	int lineNumber = data.IndexOf(line);

		//	// �ǂݍ��񂾃f�[�^
		//	var status = new BaseStatus();
		//	foreach(var value in line)
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
		//		var propMember = propType.GetMember(propertyNames[columnNumber]);

		//		// �����v���p�e�B��������ꍇ������Ȃ�
		//		if (propMember == null)
		//		{
		//			return;
		//		}

		//		if (propMember.GetType() == typeof(int))
		//		{
		//			propMember.SetValue(propMember, int.Parse(data[lineNumber][columnNumber]));
		//		}
		//		else if (propMember.GetType() == typeof(float))
		//		{
		//			//propMember.SetValue(propType, float.Parse(data[lineNumber][columnNumber]));
		//		}
		//		else
		//		{
		//			Debug.LogError("����Ȃ��^�ł�");
		//		}


		//	}
		//}
	}
}