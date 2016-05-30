using UnityEngine;
using System.Collections;

public class DieCalculator : MonoBehaviour {

	public Transform[] faces;
	public TextMesh text;
	public ParticleSystem highRollSparks;
	public ParticleSystem deathFlicker;

	Rigidbody rb;
	int dieValue = 0;
	bool dieValueHasAnimated = false;
	Transform mainCamera;
	bool countDownHasStarted = false;

	void Awake()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		text.gameObject.SetActive (false);
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}

	IEnumerator CountdownToExtinction()
	{
		countDownHasStarted = true;
        rb.isKinematic = true;

        text.gameObject.SetActive(true);
        CalculateDieValue();
        AnimateDieValue();

        yield return new WaitForSeconds(7);

		if (countDownHasStarted)
		{
			iTween.ScaleTo (gameObject, iTween.Hash ("scale", Vector3.zero, "time", 0.5f, "easetype", iTween.EaseType.easeInElastic));
			yield return new WaitForSeconds (0.35f);
			if (deathFlicker)
				deathFlicker.Emit (1);
			yield return new WaitForSeconds (0.5f);
			Destroy (gameObject);
		}
	}

	void AnimateDieValue()
	{
        //emit sparks on highest die value
		if (!dieValueHasAnimated) {
			if (dieValue == faces.Length)
                highRollSparks.Emit (120);

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
		if (rb.IsSleeping ())
        {
			KeepTextAtopDie ();
			RotateTextToFaceCamera ();
			
			if (!countDownHasStarted)
			{
				StartCoroutine ("CountdownToExtinction");
			}
		} 
		else 
		{
			countDownHasStarted = false;
			text.gameObject.SetActive (false);
			dieValueHasAnimated = false;
            rb.isKinematic = false;
		}
	}
}
