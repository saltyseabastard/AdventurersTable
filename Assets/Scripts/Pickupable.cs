using UnityEngine;
using System.Collections;

public class Pickupable : MonoBehaviour {

	Renderer rnd;
    Rigidbody rb;
	Material originalMaterial;
	public Material selectedMaterial;

	void Start()
	{
		rnd = GetComponent<Renderer> ();
        rb = GetComponent<Rigidbody>();

        if (rnd)
            originalMaterial = rnd.material;
	}

	void OnRaycastEnter(GameObject sender)
	{
        if (rnd)
		    rnd.material = selectedMaterial;
	}

	void OnRaycastExit(GameObject sender)
	{
        if (rnd)
		    rnd.material = originalMaterial;
	}

    void Update()
    {
        rb.isKinematic = rb.IsSleeping();
    }
}
