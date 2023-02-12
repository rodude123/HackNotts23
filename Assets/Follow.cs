using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{
	public bool running = true;
	
	public GameObject ball;
	public GameObject spawn;
	public float fireSpeed = 0.9f;
	private float nextActionTime;
	
	private Vector3 baseRotation;
	public float spinSpeed = 2.5f;
	public float spinAngle = 70;
	private float lastSpin;
	private float nextSpin;
	private float fullSpin;
	private float spinProg;
	
    // Start is called before the first frame update
    void Start()
    {
        Shoot();
		baseRotation = transform.localEulerAngles;
		lastSpin = Time.time;
		nextSpin = Time.time + spinSpeed;
		fullSpin = nextSpin - lastSpin;
		spinProg = (Time.time - lastSpin)/fullSpin;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime ) {
			Shoot();
			nextActionTime = Time.time + fireSpeed;
		}
		if (Time.time > nextSpin ) {
			lastSpin = Time.time;
			nextSpin = Time.time + spinSpeed;
			fullSpin = nextSpin - lastSpin;
			spinProg = (Time.time - lastSpin)/fullSpin;
		} else {
			spinProg = (Time.time - lastSpin)/fullSpin;
			spinProg = ((spinProg > .5f ? .5f-(spinProg-.5f) : spinProg)*2)-0.5f;
			Debug.Log(spinProg);
			transform.localRotation = Quaternion.Euler(baseRotation.x, (spinProg*spinAngle)+baseRotation.y, baseRotation.z);
		}
    }
	
	void Shoot()
	{
		if(running){
			GameObject newBall = Instantiate(ball, spawn.transform.position, Quaternion.identity);
			newBall.GetComponent<Rigidbody>().AddForce(spawn.transform.forward*500);
		}
	}
}