using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {
	GameObject mainCamera;
	bool carrying;
	ArrayList carriedObjects = new ArrayList();
	public float distance;
	public float smooth;
    public LayerMask layerMask;
    //CharacterController controller;
	GameObject hitObject;
	int x = Screen.width / 2;
	int y = Screen.height / 2;
	GameObject previousObject;

    // Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
    }
	
	// Update is called once per frame
	void Update () {

		//ray stuff
		Vector3 rayVector = new Vector3 (x, y, 0);

		Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(rayVector);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			hitObject = hit.collider.gameObject;
			SendMessageTo (previousObject, "OnRaycastExit");
			SendMessageTo (hitObject, "OnRaycastEnter");
			previousObject = hitObject;
		} else 
		{
			SendMessageTo (previousObject, "OnRaycastExit");
			previousObject = null;
		}

		if(carrying) {
			carry(carriedObjects);
			checkDrop();
		} else {
			pickup();
		}
	}

	void SendMessageTo(GameObject target, string message)
	{
		if (target)
			target.SendMessage (message, gameObject, SendMessageOptions.DontRequireReceiver);
	}

	void rotateObject() {
		foreach (GameObject o in carriedObjects)
		{
			o.transform.Rotate (5, 10, 15);
		}
	}

	void carry(ArrayList handful)
	{
		foreach(GameObject o in handful)
		{
			o.transform.position = Vector3.Lerp (o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
			o.transform.rotation = Quaternion.identity;
		}
	}

	void pickup() {
		if(Input.GetKeyDown (KeyCode.E)) {
			
			Pickupable p = hitObject.GetComponent<Collider>().GetComponent<Pickupable>();
			if(p != null) {
				carrying = true;
				carriedObjects.Add(p.gameObject);
				//p.gameObject.rigidbody.isKinematic = true;
				p.gameObject.GetComponent<Rigidbody>().useGravity = false;
			}
		}
	}

	void checkDrop() {
		if(Input.GetKeyDown (KeyCode.E)) {
			dropObject();
		}
	}

	void dropObject() {

        //SAVE FOR VIVE CONTROLLER
        //Vector3 horizontalVelocity = controller.velocity;
        //horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z); //consider adding in y values for vive controller
        //float horizontalSpeed = horizontalVelocity.magnitude;
        //float verticalSpeed = controller.velocity.y;
        //float overallSpeed = controller.velocity.magnitude;

        carrying = false;
		foreach (GameObject o in carriedObjects)
		{
			Rigidbody rb = o.gameObject.GetComponent<Rigidbody> ();
			rb.useGravity = true;
			rb.AddForce (transform.forward * Random.Range (3.5f, 5.5f), ForceMode.Impulse);
			rb.AddTorque (new Vector3 (Random.Range (-5.5f, 5.5f), Random.Range (-5.5f, 5.5f), Random.Range (-5.5f, 5.5f)), 
				ForceMode.Impulse);
		}
		carriedObjects = null;

	}
}
