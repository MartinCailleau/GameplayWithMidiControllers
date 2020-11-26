using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour {
    InputController input;
    private float currentTimeScale;
	// Use this for initialization
	void Start () {
        input = GetComponent<InputController>();
        currentTimeScale = 1;

    }
	
	// Update is called once per frame
	void Update () {
        currentTimeScale = input.getValue()+0.5f;
        Time.timeScale = currentTimeScale;
	}
}
