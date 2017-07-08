using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strengthMeter : MonoBehaviour
{

    public golfball parent;
    private Rigidbody _rigidbody;
    private Material _materialColored;

    // Use this for initialization
	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody>();
	    _materialColored = new Material(Shader.Find("Diffuse"));
        setPower(0);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    parent.setStrengthMeter(this);
	    _rigidbody.transform.position = parent.transform.position + Camera.main.transform.right * 0.15f;
	    //transform.LookAt(Camera.main.transform);
	    _rigidbody.transform.rotation = Camera.main.transform.rotation;
	    _rigidbody.transform.localEulerAngles = _rigidbody.transform.localEulerAngles + new Vector3(-90, 0, 0);
    }

    public void setPower(float power)
    {
        _rigidbody.transform.position = transform.position + Camera.main.transform.up * power * 0.2f * 0.5f;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, power * 0.05f * 0.5f);
 
        _materialColored.color = new Color(power / 2, 1 - power / 2, 0);
        GetComponent<Renderer>().material = _materialColored;
    }
}
