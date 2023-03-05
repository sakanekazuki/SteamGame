using UnityEngine;

public class ARelabelingVariables : PropertyAttribute
{
	public readonly string label;

	public ARelabelingVariables(string name)
	{
		label = name;
	}
}
