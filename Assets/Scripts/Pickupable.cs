using UnityEngine;
using System.Collections;

public class Pickupable : MonoBehaviour {

	Renderer renderer;
	Material originalMaterial;
	public Material selectedMaterial;

	void Start()
	{
		renderer = gameObject.GetComponent<Renderer> ();
		originalMaterial = renderer.material;
	}

	void OnRaycastEnter(GameObject sender)
	{
		renderer.material = selectedMaterial;
	}

	void OnRaycastExit(GameObject sender)
	{
		renderer.material = originalMaterial;
	}
}
