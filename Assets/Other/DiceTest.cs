using UnityEngine;
using System.Collections;

public class DiceTest : MonoBehaviour {


        public float maxTorque = 10f;
        public float maxForce = 10f;

        public Transform[] values;

        Rigidbody myRigidBody;

        bool rolling;
        // Use this for initialization
        void Start()
        {
            rolling = true;
            myRigidBody = GetComponent<Rigidbody>();
            myRigidBody.maxAngularVelocity = 30;
        }

        // Update is called once per frame
        void Update()
        {
            if (rolling)
            {
                if (myRigidBody.IsSleeping())
                {
                    rolling = false;
                    int result = GetResult();
                    Debug.Log(result);
                }
            }
        }

        int GetResult()
        {

            //int result;
            for (int i = 0; i < values.Length; i++)
            {
                Transform t = values[i];
                float dotProduct = Vector3.Dot(Vector3.up, t.up);

                if (dotProduct > 0.98f) //I like to give it a little wiggle room.
                {
                    return i + 1;
                }
            }

            Debug.LogWarning("DiceRoll failed");
            return 0;
        }

        //[ContextMenu("Roll Dice")]
        public void Roll()
        {
            transform.position = new Vector3(0f, 1f, -4.5f);

            myRigidBody.AddForce(Vector3.forward * Random.Range(0.2f, maxForce));

            myRigidBody.AddTorque(new Vector3(
                Random.Range(-1f * maxTorque, maxTorque),
                Random.Range(-1f * maxTorque, maxTorque),
                Random.Range(-1f * maxTorque, maxTorque)));

            rolling = true;
        }
    }
