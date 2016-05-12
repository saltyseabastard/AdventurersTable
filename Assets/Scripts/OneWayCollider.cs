using UnityEngine;
using System.Collections;

public class OneWayCollider : MonoBehaviour {

    public BoxCollider wall;

    void OnTriggerEnter(Collider hitObject)
    {
        //Dice will always be on layer 8
        if (hitObject.gameObject.layer == 8)
        {
            wall.enabled = false;
            print("enter collider");
        }
    }

    void OnTriggerExit(Collider hitObject)
    {
        if (hitObject.gameObject.layer == 8)
        {
            wall.enabled = true;
            print("exit collider");
        }
    }
}

