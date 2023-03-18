[System.Serializable]
public class BaseStatus : BaseData
{
	public int hp = 0;
	public int HP { get => hp; set => hp = value; }

	public int attack = 0;
	public int Attack { get => attack; set => attack = value; }

	public int defence = 0;
	public int Defence { get => defence; set => defence = value; }

	public int speed = 0;
	public int Speed { get => speed; set => speed = value; }
}