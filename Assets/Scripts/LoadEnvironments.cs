using UnityEngine;
using System.Collections;

public class LoadEnvironments : MonoBehaviour {

    private GameObject environment;
    private string[] list = { "/Environments/BlackSmith","/Environments/BlackSmith2" };

    // Use this for initialization
    void Start()
    {
        foreach (string element in list)
        {
            environment = GameObject.Find(element);
            SetTargetInvisible(environment);
        }

        environment = GameObject.Find("/Environments/BlackSmith2");
        SetTargetInvisible(environment);

        environment = GameObject.Find("/Environments/BlackSmith");
        SetTargetVisible(environment);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetTargetInvisible(environment);
            environment = GameObject.Find("/Environments/BlackSmith2");
            SetTargetVisible(environment);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetTargetInvisible(environment);
            environment = GameObject.Find("/Environments/BlackSmith");
            SetTargetVisible(environment);
        }
    }

    //Turns off an environment
    void SetTargetInvisible(GameObject Target)
    {
        Component[] gameObjects = Target.GetComponentsInChildren(typeof(Renderer));
        Light[] lights = Target.GetComponentsInChildren<Light>(true);
        foreach (Component i in gameObjects)
        {
            Renderer scene = (Renderer)i;
            scene.enabled = false;
        }
        foreach (Light i in lights)
        {
            i.enabled = false;
        }
    }

    //Turns on an environment
    void SetTargetVisible(GameObject Target)
    {
        Component[] gameObjects = Target.GetComponentsInChildren(typeof(Renderer));
        Light[] lights = Target.GetComponentsInChildren<Light>(true);
        foreach (Component i in gameObjects)
        {
            Renderer scene = (Renderer)i;
            scene.enabled = true;
        }
        foreach (Light i in lights)
        {
            i.enabled = true;
        }
    }
}
