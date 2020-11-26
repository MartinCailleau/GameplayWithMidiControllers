using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithSensor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = (Vector3.up * int.Parse(SerialReader._instance.value))/4;
	}
}
