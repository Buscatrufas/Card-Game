using UnityEngine;
using System.Collections;

public class ComboBoxTest : MonoBehaviour
{
	GUIContent[] comboBoxList;
	private ComboBox comboBoxControl;// = new ComboBox();
	private GUIStyle listStyle = new GUIStyle();
	
	private void Start()
	{
		comboBoxList = new GUIContent[4];
		comboBoxList[0] = new GUIContent("Guardian");
		comboBoxList[1] = new GUIContent("Asesino");
		comboBoxList[2] = new GUIContent("Hechicero");
		comboBoxList[3] = new GUIContent("Clerigo");
		
		listStyle.normal.textColor = Color.white; 
		listStyle.onHover.background =
			listStyle.hover.background = new Texture2D(2, 2);
		listStyle.padding.left =
			listStyle.padding.right =
				listStyle.padding.top =
				listStyle.padding.bottom = 4;
		
		comboBoxControl = new ComboBox(new Rect(300, 190, 200, 30), comboBoxList[0], comboBoxList, "button", "box", listStyle);

	}
	
	private void OnGUI () 
	{
		comboBoxControl.Show ();

	}

	public int getValueComboBox(){
		return comboBoxControl.SelectedItemIndex;
	}
}