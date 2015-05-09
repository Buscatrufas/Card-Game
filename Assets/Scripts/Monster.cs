using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Monster {

	[XmlAttribute("name")]
	public string Name;
	
	public int Health;
	public int Atk;
	public int Cost;

	public string getName(){ return Name; }
	public int getHealth(){ return Health; }
	public int getAtk(){ return Atk; }
	public int getCost(){ return Cost; }

}
