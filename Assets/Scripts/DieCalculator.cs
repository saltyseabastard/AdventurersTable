using UnityEngine;
using System.Collections;

public class DieCalculator : MonoBehaviour {

	public Transform[] faces;
	public TextMesh text;

	int dieValue = 0;
	void Update()
	{
		float highestY = -Mathf.Infinity;

		for (int i = 0; i < faces.Length; i++) 
		{
			if (faces [i].position.y > highestY) {
				highestY = faces[i].position.y;
				dieValue = i + 1;

			}
		}

		text.text = dieValue.ToString();
	}
}
