using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    public Transform respawnPoint;
    private InputController input;
    private Transform ball;
    // Use this for initialization
    void Start () {
        input = GetComponent<InputController>();
        ball = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
       if( input.getValue() > 0.5f)
        {
            ball.position = respawnPoint.position;
            ball.GetComponent<Propulsor>().reInit();
        }

    }
}
