using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public enum ButtonType
	{
		Dice,
		Environment
	}

	public ButtonType buttonType;
	public int diceSides; 

}
