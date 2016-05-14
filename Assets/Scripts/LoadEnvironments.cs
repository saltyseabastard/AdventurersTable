using UnityEngine;
using System.Collections;

public class LoadEnvironments : MonoBehaviour
{

    private GameObject environment;
    public GameObject[] environ;
    public Material[] MaterialRef;

    void Start()
    {

        foreach (GameObject go in environ)
        {
            go.SetActive(false);
        }
        environ[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            foreach (GameObject go in environ)
            {
                go.SetActive(false);
            }
            environ[0].SetActive(true);
            RenderSettings.skybox = MaterialRef[0];
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (GameObject go in environ)
            {
                go.SetActive(false);
            }
            environ[1].SetActive(true);
            RenderSettings.skybox = MaterialRef[1];
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (GameObject go in environ)
            {
                go.SetActive(false);
            }
            environ[2].SetActive(true);
            RenderSettings.skybox = MaterialRef[2];
        }
    }
}
