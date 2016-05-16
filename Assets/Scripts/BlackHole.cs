using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		collision.transform.parent = transform.parent;
	}
}
