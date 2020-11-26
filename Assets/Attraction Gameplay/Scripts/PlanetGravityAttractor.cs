using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputController))]
public class PlanetGravityAttractor : MonoBehaviour {

    public float scaleFactor;

    public bool activeOscSend;
    public OSC osc;

    private InputController inputController;
    private float gravity = -1;
    private float inputValue;
	// Use this for initialization
	void Start () {
        inputController = GetComponent<InputController>();
    }
	
	// Update is called once per frame
	void Update () {
        inputValue = inputController.getValue() -0.5f;
        gravity = -1 * inputValue * scaleFactor;
        transform.localScale = Vector3.one * inputValue * scaleFactor * 0.2f;
        if (activeOscSend)
        {
            OscMessage message = new OscMessage();
            message.address = "/Attractor_force_" + gameObject.name;
            message.values.Add(inputValue);
            osc.Send(message);
        }
        colorFeedback();
    }

    public void colorFeedback()
    {
        Material mat = GetComponent<Renderer>().material;
        
        if( gravity > 0)
            mat.SetColor("_Color",Color.red);
        else if( gravity == 0)
            mat.SetColor("_Color", Color.white);
        else if( gravity < 0)
            mat.SetColor("_Color", Color.green);
    }

    public void attract(Transform body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
        Debug.Log("Attraction value : " + (body.position - transform.position).magnitude);
        body.GetComponent<Rigidbody>().AddForce(gravityUp* gravity * Mathf.Clamp((1- (body.position - transform.position).magnitude/5),0,1));
      
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
