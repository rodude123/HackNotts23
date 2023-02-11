using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Control_Script : MonoBehaviour
{

    public OSC osc; //racket must be used in this field
    private float multi = 30;
    [SerializeField] private Transform racket;
    [SerializeField] private Transform camera;


    // On start, an OSC message address is defined as well as the required function.
    void Start()
    {

        osc.SetAddressHandler("/gyrosc/gyro/gyro", gyroHandler);

    }


    /* Function receieves the necessary OSC  message and uses two values to control swing and pitch/various rotation (whatever best describes it).
     The values are multiplied to gain further range of movement. Values are applied to the rotation of the racket for swinging. Swing value is used
        for position for more free hand movement - similar to movement in VR. Y and Z axis of the racket (in terms of position) are clamped to the
        camera's position which prevents OVERLY free movement.*/
    void gyroHandler(OscMessage message)
    {

        float pitchGyro = message.GetFloat(0) * multi;
        float gyroX = message.GetFloat(1);
        float swingGyro = -message.GetFloat(2) * multi;


        racket.rotation = Quaternion.Euler(-pitchGyro, swingGyro, 0);
        racket.position = new Vector3(swingGyro / 100, camera.position.y, camera.position.z + 2);
    }
}
