using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Physics.IgnoreLayerCollision(0, 6);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calculate your desired position however you like.
        Vector3 desiredPosition = target.transform.position;

        // What velocity gets us there in one timestep?
        Vector3 desiredVelocity = (desiredPosition - transform.position) / Time.deltaTime;


        // Apply a sufficient impulse to hit reach that velocity.
        rb.AddForce(desiredVelocity - rb.velocity, ForceMode.VelocityChange);

        Quaternion diff = Quaternion.Inverse(rb.rotation) * target.transform.rotation;
        Vector3 eulers = OrientTorque(diff.eulerAngles);
        Vector3 torque = eulers;
        //put the torque back in body space
        torque = rb.rotation * torque;

        //just zero out the current angularVelocity so it doesnt interfere
        rb.angularVelocity = Vector3.zero;

        rb.AddTorque(torque, ForceMode.VelocityChange);
    }

    private Vector3 OrientTorque(Vector3 torque)
    {
        // Quaternion's Euler conversion results in (0-360)
        // For torque, we need -180 to 180.

        return new Vector3
        (
        torque.x > 180f ? torque.x - 360f : torque.x,
        torque.y > 180f ? torque.y - 360f : torque.y,
        torque.z > 180f ? torque.z - 360f : torque.z
        );
    }

}