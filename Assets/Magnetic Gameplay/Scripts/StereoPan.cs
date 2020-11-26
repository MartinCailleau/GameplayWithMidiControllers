using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StereoPan : MonoBehaviour {
    public InputController rightEffector,leftEffector;
    private AudioSource audioSource;
    private float stereoPanValue;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        stereoPanValue = -(leftEffector.getValue()*0.7f) + (rightEffector.getValue()*0.7f);
        audioSource.panStereo = stereoPanValue;  

    }
}
