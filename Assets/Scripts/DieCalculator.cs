using UnityEngine;
using System.Collections;

public class DieCalculator : MonoBehaviour {

	public Transform[] faces;
	public TextMesh text;
	public ParticleSystem particleSystem;

	Rigidbody rb;
	int dieValue = 0;
	bool dieValueHasAnimated = false;

	void Awake()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		//particleSystem = gameObject.GetComponent<ParticleSystem> ();
		text.gameObject.SetActive (false);
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

	void Update()
	{
		if (rb.IsSleeping ()) {
			text.gameObject.SetActive (true);

			float highestY = -Mathf.Infinity;

			for (int i = 0; i < faces.Length; i++) {
				if (faces [i].position.y > highestY) {
					highestY = faces [i].position.y;
					dieValue = i + 1;

				}
			}

			text.text = dieValue.ToString ();
			AnimateDieValue ();
		} 
		else 
		{
			text.gameObject.SetActive (false);
			dieValueHasAnimated = false;
		}
	}
}
