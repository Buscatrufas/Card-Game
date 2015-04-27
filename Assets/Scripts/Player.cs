using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private int health;
	private string namePlayer;
	private GameObject[] card;
	private string classPlayer;

	public Player(string _name, string _classPlayer){
		namePlayer = _name;
		classPlayer = _classPlayer;
		health = 30;
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

}
