using UnityEngine;
using System.Collections;

public class DieCalculator : MonoBehaviour {

	public Transform[] faces;
	public TextMesh text;
	public ParticleSystem particleSystem;

	Rigidbody rb;
	int dieValue = 0;
	bool dieValueHasAnimated = false;
	Transform mainCamera;

	void Awake()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		text.gameObject.SetActive (false);
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}

	void AnimateDieValue()
	{
		if (!dieValueHasAnimated) {
			if (particleSystem && dieValue == faces.Length)
				particleSystem.Emit (120);
			iTween.ScaleFrom (text.gameObject, iTween.Hash ("x", 0.1f, "y", 0.1f, "time", 0.5f, "easetype", iTween.EaseType.easeOutElastic));
			dieValueHasAnimated = true;
		}
	}

	void RotateTextToFaceCamera()
	{
		var n = mainCamera.position - text.transform.position; 
		text.transform.rotation = Quaternion.LookRotation (-n);
	}

	void KeepTextAtopDie()
	{
		text.transform.position = new Vector3 (transform.position.x, transform.position.y + 0.2f, transform.position.z);
	}

	void CalculateDieValue()
	{
		float highestY = -Mathf.Infinity;

		for (int i = 0; i < faces.Length; i++) {
			if (faces [i].position.y > highestY) {
				highestY = faces [i].position.y;
				dieValue = i + 1;

			}
		}

		text.text = dieValue.ToString ();
	}

	void Update()
	{
		if (rb.IsSleeping ()) {
			text.gameObject.SetActive (true);

			KeepTextAtopDie ();
			RotateTextToFaceCamera ();
			CalculateDieValue ();
			AnimateDieValue ();
		} 
		else 
		{
			text.gameObject.SetActive (false);
			dieValueHasAnimated = false;
		}
	}
}
