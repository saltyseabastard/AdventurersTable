using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PickupObject : MonoBehaviour
{
	GameObject mainCamera;
	bool carrying;
	ArrayList carriedObjects = new ArrayList ();
	public float distance;
	public float smooth;
	public LayerMask layerMask;
	GameObject hitObject;
	int x = Screen.width / 2;
	int y = Screen.height / 2;
	GameObject previousObject;
	public GameObject blackHole;
    SteamVR_TrackedObject trackedController;

    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        trackedController = GetComponent<SteamVR_TrackedObject>();

        //listen for relevant controller events
        if (GetComponent<SteamVR_ControllerEvents>() == null)
        {
            Debug.LogWarning("SteamVR_ControllerEvents_ListenerExample is required to be attached to a SteamVR Controller that has the SteamVR_ControllerEvents script attached to it");
            return;
        }

        //Setup controller event listeners
		SteamVR_ControllerEvents svr;
		//Setup controller event listeners
		if (GetComponent<SteamVR_ControllerEvents> ())
		{
			svr = GetComponent<SteamVR_ControllerEvents> ();
			svr.TriggerClicked += new ControllerClickedEventHandler (DoTriggerClicked);
			svr.TriggerUnclicked += new ControllerClickedEventHandler (DoTriggerUnclicked);
		}
    
    }

    void DoTriggerClicked(object sender, ControllerClickedEventArgs e)
    {
        //DebugLogger(e.controllerIndex, "TRIGGER", "pressed down", e.buttonPressure, e.touchpadAxis);
        //Debug.Log("Trigger clicked");
    }

    void DoTriggerUnclicked(object sender, ControllerClickedEventArgs e)
    {
        ReleaseObjectWithForce((int)e.controllerIndex);   
    }

    void OnDisable()
    {
		SteamVR_ControllerEvents svr;
        //Setup controller event listeners
		if (GetComponent<SteamVR_ControllerEvents> ())
		{
			svr = GetComponent<SteamVR_ControllerEvents> ();
			svr.TriggerClicked -= new ControllerClickedEventHandler (DoTriggerClicked);
			svr.TriggerUnclicked -= new ControllerClickedEventHandler (DoTriggerUnclicked);
		}
    }

	// Update is called once per frame
	void Update ()
    {
		//ray stuff
		Vector3 rayVector = new Vector3 (x, y, 0);

		Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(rayVector);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			hitObject = hit.collider.gameObject;
			SendMessageTo (previousObject, "OnRaycastExit");
			SendMessageTo (hitObject, "OnRaycastEnter");
			previousObject = hitObject;
		}
        else 
		{
			SendMessageTo (previousObject, "OnRaycastExit");
			previousObject = null;
		}

		if(carrying) 
		{
			carry (carriedObjects);
			checkDrop();
		}
	 	else 
		{
			pickup();
		}
	}

	void SendMessageTo(GameObject target, string message)
	{
		if (target)
			target.SendMessage (message, gameObject, SendMessageOptions.DontRequireReceiver);
	}

	void carry(ArrayList objects) 
	{
		blackHole.SetActive (true);
	}

	void pickup() 
	{
		if(Input.GetKeyDown (KeyCode.E)) 
		{
			AddObjectToHand (hitObject);
		}
	}

	void checkDrop() 
	{
		if(Input.GetKeyDown (KeyCode.R) && carriedObjects.Count > 0) {
			DropObject();
		}
	}

    void ReleaseObjectWithForce(int deviceIndex)
    {
        //get force from velocity - SAVE THIS FOR VIVE CONTROLLERS
        //Rigidbody device = gameObject.GetComponent<Rigidbody>();
        var device = SteamVR_Controller.Input(deviceIndex);
        var origin = trackedController.origin ? trackedController.origin : trackedController.transform.parent;

        blackHole.SetActive(false);
        carrying = false;
        for (int i = carriedObjects.Count - 1; i >= 0; i--)
        {
            GameObject go = (GameObject)carriedObjects[i];
            Rigidbody rb = go.GetComponent<Rigidbody>();

            go.transform.parent = null;
            rb.useGravity = true;

            //switch dice from ignore raycast to dice layer for table interaction
            go.layer = 8;

            rb.velocity = origin.TransformVector(
                new Vector3(device.velocity.x + Random.Range(-0.33f, 0.33f),
                            device.velocity.y + Random.Range(0, 1f),
                            device.velocity.z + Random.Range(0, 1f)));

            rb.angularVelocity = origin.TransformVector(
                new Vector3(device.angularVelocity.x + Random.Range(-1, 1),
                            device.angularVelocity.x + Random.Range(-1, 1),
                            device.angularVelocity.x + Random.Range(-1, 1)));

            carriedObjects.Remove(carriedObjects[i]);
        }
    }

    void DropObject() 
	{
        blackHole.SetActive(false);
        carrying = false;
		for (int i = carriedObjects.Count-1; i >= 0; i--)
		{
			GameObject go = (GameObject) carriedObjects [i];
			Rigidbody rb = go.GetComponent<Rigidbody> ();
			
			go.transform.parent = null;
			rb.useGravity = true;
			rb.AddForce (transform.forward * Random.Range (3.5f, 5.5f), ForceMode.Impulse);
			rb.AddTorque (new Vector3 (Random.Range (-5.5f, 5.5f), Random.Range (-5.5f, 5.5f), Random.Range (-5.5f, 5.5f)), 
				ForceMode.Impulse);

            //switch dice from ignore raycast to dice layer for table interaction
            go.layer = 8;
			carriedObjects.Remove (carriedObjects[i]);
		}
	}

	public void AddObjectToHand(GameObject go)
	{
		Pickupable p = go.GetComponent<Collider>().GetComponent<Pickupable>();
		if(p != null) {
			carrying = true;
			carriedObjects.Add(p.gameObject);
			p.gameObject.GetComponent<Rigidbody>().useGravity = false;
		}
	}
}