using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

public class MyDropDown : AdvancedDropdown
{
	List<string> name = new List<string>();

    public MyDropDown(AdvancedDropdownState state)
		: base(state)
	{
		name.Add("a");
		name.Add("b");
		name.Add("c");
		name.Add("d");
		var minSize = minimumSize;
		minSize.y = 200;
		minimumSize = minSize;
	}

	protected override AdvancedDropdownItem BuildRoot()
	{
		var root = new AdvancedDropdownItem("Root");

		foreach(var e in name)
		{
			root.AddChild(new AdvancedDropdownItem(e));
		}

		return root;
	}

	public void AddName(string s)
	{
		name.Add(s);
	}
}
