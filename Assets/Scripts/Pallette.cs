using UnityEngine;
using System.Collections;

public class Pallette : MonoBehaviour {

	public Button[] face1Buttons;
	public Button[] face2Buttons;
	public Button[] face3Buttons;
	public Button[] face4Buttons;
	public GameObject[] dicePrefabs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("1")) {

			Instantiate (dicePrefabs [0]);
		}
	}
}
