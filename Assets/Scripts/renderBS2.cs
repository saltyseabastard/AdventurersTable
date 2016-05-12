using UnityEngine;
using System.Collections;

public class renderBS2 : MonoBehaviour
{

    private GameObject bs2;
    // Use this for initialization
    void Start()
    {
        bs2 = GameObject.Find("/Environments/BlackSmith2");
        bs2.SetActive(false);
        //bs2.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // show
            //bs2.GetComponent<Renderer>().enabled = true;
            bs2.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            // hide
            //bs2.GetComponent<Renderer>().enabled = false;
            bs2.SetActive(false);
        }
    }
}


