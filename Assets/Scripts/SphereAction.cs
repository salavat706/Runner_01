using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAction : MonoBehaviour {

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //rb.AddForce(Vector3.forward * 10);
        //transform.Translate(Vector3.forward * Time.deltaTime * 5);
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Cube")
        {
            transform.position = new Vector3( 0, transform.position.y, 0);
        }
    }
}
