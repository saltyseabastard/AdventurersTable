using UnityEngine;
using System.Collections;

public class OrientDiceCount : MonoBehaviour {

	Quaternion rotation;

	void Awake()
	{
		rotation = transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		transform.rotation = rotation;
	
	}
}
