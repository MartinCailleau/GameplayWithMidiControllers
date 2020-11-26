using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravityBody : MonoBehaviour {

    public PlanetGravityAttractor[] attractors;
    private Transform myTransform;
    Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (PlanetGravityAttractor attractor in attractors)
        {
            attractor.attract(transform);
        }
        
	}
}
