using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

	Transform mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").transform;

	}
	
	// Update is called once per frame
	void Update () {

		var n = mainCamera.position - transform.position; 
		transform.rotation = Quaternion.LookRotation( -n ); 
	
	}
}
