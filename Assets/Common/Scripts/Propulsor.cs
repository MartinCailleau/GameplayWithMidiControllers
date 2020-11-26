using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputController))]
public class Propulsor : MonoBehaviour {
    public float force;
    public bool useDirectionSelector = true;
    public DirectionSelector dirSelector;

    private Rigidbody body;
    private bool launched = false;
    private InputController input;
    private Vector3 direction;
    private PlanetGravityBody planetGravityBody;
    // Use this for initialization
    void Start () {
        direction = Vector3.zero;
        body = GetComponent<Rigidbody>();
        input = GetComponent<InputController>();
        planetGravityBody = GetComponent<PlanetGravityBody>();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(input.getValue());
        if(input.getValue() > 0.5f && !launched)
        {
            launch();
            launched = true;
        }
        if (launched)
        {
            //body.AddForce(direction * force/2f, ForceMode.Force);
        }
    }

    void launch()
    {
        Debug.Log("Launched");

        if (useDirectionSelector)
        {
            direction = dirSelector.getDirection();
        }else
        {
            direction = transform.forward;
        }
        
        Debug.Log("Launched in direction : " + direction);
        body.AddForce(direction * force, ForceMode.Impulse);
        if(planetGravityBody)
            planetGravityBody.enabled = true;
    }

    public void reInit()
    {
        body.velocity = Vector3.zero;
        if(GetComponent<PlanetGravityBody>())
            GetComponent<PlanetGravityBody>().enabled = false;

        launched = false;
    }
}
