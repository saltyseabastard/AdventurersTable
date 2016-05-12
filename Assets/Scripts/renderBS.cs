using UnityEngine;
using System.Collections;

public class renderBS : MonoBehaviour {

    private GameObject bs;
    // Use this for initialization
    void Start () {
        bs = GameObject.Find("/Environments/BlackSmith");
        //bs.GetComponent<Renderer>().enabled = true;
        bs.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // show
            //bs.GetComponent<Renderer>().enabled = true;
            bs.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {   
            // hide
            //bs.GetComponent<Renderer>().enabled = false;
            bs.SetActive(false);
        }
    }
}


