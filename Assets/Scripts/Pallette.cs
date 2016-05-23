using UnityEngine;
using System.Collections;

public class Pallette : MonoBehaviour {

	public LoadEnvironments envLoader;
	public GameObject[] diceButtons;
	public GameObject[] dicePrefabs;

	//the pickupObject script on the character controller
	public PickupObject pickupObject;
	int faceUp = 0;

	void SpawnDice()
	{
		if(Input.GetKeyDown (KeyCode.Alpha1))
		{
			GameObject d4 = (GameObject) Instantiate(dicePrefabs[0], transform.position, transform.rotation);
			pickupObject.AddObjectToHand(d4);
			//TODO: animate button
		}
		if(Input.GetKeyDown (KeyCode.Alpha2))
		{
			GameObject d6 = (GameObject) Instantiate(dicePrefabs[1], transform.position, transform.rotation);
			pickupObject.AddObjectToHand(d6);
		}
		if(Input.GetKeyDown (KeyCode.Alpha3))
		{
			GameObject d8 = (GameObject) Instantiate(dicePrefabs[2], transform.position, transform.rotation);
			pickupObject.AddObjectToHand(d8);
		}
		if(Input.GetKeyDown (KeyCode.Alpha4))
		{
			//TODO: Multiple colors of dice here
			GameObject d10 = (GameObject) Instantiate(dicePrefabs[3], transform.position, transform.rotation);
			pickupObject.AddObjectToHand(d10);
		}
		if(Input.GetKeyDown (KeyCode.Alpha5))
		{
			GameObject d12 = (GameObject) Instantiate(dicePrefabs[4], transform.position, transform.rotation);
			pickupObject.AddObjectToHand(d12);
		}
		if(Input.GetKeyDown (KeyCode.Alpha6))
		{
			GameObject d20 = (GameObject) Instantiate(dicePrefabs[5], transform.position, transform.rotation);
			pickupObject.AddObjectToHand(d20);
		}
	}

	void AnimateButton(GameObject button)
	{
		//iTween.MoveFrom(
	}

	void SwapEnvironments()
	{
		if(Input.GetKeyDown (KeyCode.Alpha1))
		{
			envLoader.ChangeEnvironment (LoadEnvironments.Environment.Blacksmith);
			//TODO: animate button
		}
		if(Input.GetKeyDown (KeyCode.Alpha2))
		{
			envLoader.ChangeEnvironment (LoadEnvironments.Environment.Forest);
		}
		if(Input.GetKeyDown (KeyCode.Alpha3))
		{
			envLoader.ChangeEnvironment (LoadEnvironments.Environment.Stonehenge);
		}
	}

	void RotatePallette()
	{
		if (Input.GetKeyDown (KeyCode.L))
		{
			//transform.Rotate(new Vector3 (0, 90, 0), Space.Self);
			iTween.RotateBy(gameObject, iTween.Hash("y", -0.25f, "islocal", true, "time", 0.25f, "easetype", iTween.EaseType.easeOutQuad));

			faceUp = faceUp == 3 ? 0 : faceUp + 1;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		RotatePallette ();

		switch (faceUp)
		{
		case 0: 
			SpawnDice ();
			break;
		
		case 1:
			SwapEnvironments ();
			break;
		}
	}
}
