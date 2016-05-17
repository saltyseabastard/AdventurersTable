using UnityEngine;
using System.Collections;

public class LoadEnvironments : MonoBehaviour
{

    public string[] environments;
    public Material[] MaterialRef;
    public GameObject instance;
    public Vector3[] originalPos;

    void Start()
    {
        instance = Instantiate(Resources.Load(environments[0], typeof(GameObject))) as GameObject;
        instance.transform.parent = transform;
        instance.transform.localPosition = originalPos[0];
        instance.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Destroy(instance);
            RenderSettings.skybox = MaterialRef[0];
            instance = Instantiate(Resources.Load(environments[0], typeof(GameObject))) as GameObject;
            instance.transform.parent = transform;
            instance.transform.localPosition = originalPos[0];
            instance.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Destroy(instance);
            RenderSettings.skybox = MaterialRef[1];
            instance = Instantiate(Resources.Load(environments[1], typeof(GameObject))) as GameObject;
            instance.transform.parent = transform;
            instance.transform.localPosition = originalPos[1];
            instance.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Destroy(instance);
            RenderSettings.skybox = MaterialRef[2];
            instance = Instantiate(Resources.Load(environments[2], typeof(GameObject))) as GameObject;
            instance.transform.parent = transform;
            instance.transform.localPosition = originalPos[2];
            instance.SetActive(true);
        }
    }
}
