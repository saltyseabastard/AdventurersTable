using UnityEngine;
using System.Collections;

public class Pallette : MonoBehaviour {

	public GameObject[] diceButtons;
	public GameObject[] dicePrefabs;

	//the pickupObject script on the character controller
	public PickupObject pickupObject;

	void SpawnDice()
	{
		if(Input.GetKeyDown (KeyCode.Alpha1))
		{
			GameObject d4 = (GameObject) Instantiate(dicePrefabs[0], new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			pickupObject.AddObjectToHand(d4);
			//TODO: animate button
		}
		if(Input.GetKeyDown (KeyCode.Alpha2))
		{
			GameObject d6 = (GameObject) Instantiate(dicePrefabs[1], new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			pickupObject.AddObjectToHand(d6);
		}
		if(Input.GetKeyDown (KeyCode.Alpha3))
		{
			GameObject d8 = (GameObject) Instantiate(dicePrefabs[2], new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			pickupObject.AddObjectToHand(d8);
		}
		if(Input.GetKeyDown (KeyCode.Alpha4))
		{
			//TODO: Multiple colors of dice here
			GameObject d10 = (GameObject) Instantiate(dicePrefabs[3], new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			pickupObject.AddObjectToHand(d10);
		}
		if(Input.GetKeyDown (KeyCode.Alpha5))
		{
			GameObject d12 = (GameObject) Instantiate(dicePrefabs[4], new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			pickupObject.AddObjectToHand(d12);
		}
		if(Input.GetKeyDown (KeyCode.Alpha6))
		{
			GameObject d20 = (GameObject) Instantiate(dicePrefabs[5], new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			pickupObject.AddObjectToHand(d20);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		SpawnDice ();
	}
}
