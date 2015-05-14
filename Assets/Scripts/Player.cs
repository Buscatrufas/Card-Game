using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private int health;
	private string namePlayer;
	private string classPlayer;
	private int numZaphires;
	private int costPerTurn;

	public Player(string _name, string _classPlayer){
		namePlayer = _name;
		classPlayer = _classPlayer;
		health = 20;
		numZaphires = 0;
	}

	public int Health{
		get{
			return this.health;
		}
		set{
			health = value;
		}
	}

	public string NamePlayer{
		get{
			return this.namePlayer;
		}
		set{
			namePlayer = value;
		}
	}

	public string ClassPlayer{
		get{
			return this.classPlayer;
		}
		set{
			classPlayer = value;
		}
	}

	public int Zaphires{
		get{
			return this.numZaphires;
		}set{
			numZaphires = value;
		}
	}

	public string getNamePlayer(){
		return namePlayer;
	}

	public int CostInTurn{
		get{
			return this.costPerTurn;
		}set{
			costPerTurn = value;
		}
	}

}
