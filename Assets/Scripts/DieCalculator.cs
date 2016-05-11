using UnityEngine;
using System.Collections;

public class DieCalculator : MonoBehaviour {

	public Transform[] faces;
	public TextMesh text;

	Rigidbody rb;
	int dieValue = 0;
	bool dieValueHasAnimated = false;

	void Awake()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		text.gameObject.SetActive (false);
	}

	void AnimateDieValue()
	{
		if (!dieValueHasAnimated) {
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
