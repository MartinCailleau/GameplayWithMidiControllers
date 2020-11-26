using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionSelector : MonoBehaviour {

    private InputController input;
    private float inputValue;
    private Quaternion orientation;
	// Use this for initialization
	void Start () {
        orientation = new Quaternion();
        input = GetComponent<InputController>();
    }
	
	// Update is called once per frame
    
	void Update () {
        inputValue = input.getValue() -0.5f;
            transform.rotation = Quaternion.Euler(0, 360f * input.getValue()-90, 0);
               
	}

    public Vector3 getDirection()
    {
        return transform.forward;
    }
}
