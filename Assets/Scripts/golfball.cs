using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golfball : MonoBehaviour
{
    private Rigidbody body;

    private bool mousePressed = false;
    private double strength = 0.0f;
    private DateTime mouseDownStart = DateTime.Now;

    public strengthMeter strengthMeterClass;
    private strengthMeter meter;

	// Use this for initialization
	void Start ()
	{
	    body = GetComponent<Rigidbody>();
	    meter = Instantiate<strengthMeter>(strengthMeterClass, transform.position, Quaternion.identity);
	    meter.parent = this;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
	    {
	        if (body.velocity.Equals(new Vector3(0, 0, 0)))
	        {
	            mousePressed = true;
	            mouseDownStart = DateTime.Now;
	            strength = Mathf.Clamp((float) (DateTime.Now - mouseDownStart).TotalSeconds, 0.1f, 2);
	            Debug.Log("Pressed left click.");
	        }
	    }
        else if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) && mousePressed)
	    {
	        mousePressed = false;
	        strength = Mathf.Clamp((float)(DateTime.Now - mouseDownStart).TotalSeconds, 0.1f, 2);
	        Debug.Log(strength);
	       // body.AddForce(Camera.main.transform.TransformDirection(new Vector3(0, 0, (float)strength)),ForceMode.Impulse);
	        body.AddForce(new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * (float)strength,ForceMode.Impulse);
	        GameManager.Instance.addHit();
	    }
        else if (mousePressed)
	    {
	        strength = Mathf.Clamp((float)(DateTime.Now - mouseDownStart).TotalSeconds, 0.1f, 2);
        }
	    else
	    {
	        strength = 0;
	    }

	    meter.setPower((float)strength);

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed RIGHT click.");
            GameManager.Instance.RespawnBall();
        }

	    if (transform.position.y < 0)
	    {
	        GameManager.Instance.RespawnBall();
	    }

    }

    internal void Kill()
    {
        GameObject.Destroy(meter);
        GameObject.Destroy(gameObject);
    }

    public void setStrengthMeter(strengthMeter strengthMeter)
    {
        meter = strengthMeter;
    }
}
