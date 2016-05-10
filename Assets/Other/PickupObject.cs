using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {
	GameObject mainCamera;
	bool carrying;
	GameObject carriedObject;
	public float distance;
	public float smooth;
    CharacterController controller;

    // Use this for initialization
    void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
        //rigidBody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
		if(carrying) {
			carry(carriedObject);
			checkDrop();
			//rotateObject();
		} else {
			pickup();
		}
	}

	void rotateObject() {
		carriedObject.transform.Rotate(5,10,15);
	}

	void carry(GameObject o) {
		o.transform.position = Vector3.Lerp (o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
		o.transform.rotation = Quaternion.identity;
	}

	void pickup() {
		if(Input.GetKeyDown (KeyCode.E)) {
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				Pickupable p = hit.collider.GetComponent<Pickupable>();
				if(p != null) {
					carrying = true;
					carriedObject = p.gameObject;
					//p.gameObject.rigidbody.isKinematic = true;
					p.gameObject.GetComponent<Rigidbody>().useGravity = false;
				}
			}
		}
	}

	void checkDrop() {
		if(Input.GetKeyDown (KeyCode.E)) {
			dropObject();
		}
	}

	void dropObject() {

        //get force from velocity
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z); //consider adding in y values for vive controller
        float horizontalSpeed = horizontalVelocity.magnitude;
        float verticalSpeed = controller.velocity.y;
        float overallSpeed = controller.velocity.magnitude;



        carrying = false;
        Rigidbody rb = carriedObject.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(horizontalVelocity * overallSpeed/5, ForceMode.Impulse);
        
		carriedObject = null;

	}
}
