using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetOSC : MonoBehaviour {
    public GameObject obj;
    public float power;
    public bool activeOSC;
    public OSC osc;

    private float magneticForce;
    private InputController input;
    private ParticleSystem ps;    
	// Use this for initialization
	void Start () {
        input = GetComponent<InputController>();
        ps = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        magneticForce = input.getValue() * power;
        obj.GetComponent<Rigidbody>().AddForce((transform.position - obj.transform.position).normalized * magneticForce,ForceMode.Acceleration);

        if (ps)
            ps.transform.localScale = Vector3.one * (1 + input.getValue());

        if (activeOSC)
        {
            OscMessage msg = new OscMessage();
            msg.address = "/Magnet_" + gameObject.name;
            msg.values.Add(input.getValue());
            osc.Send(msg);
        }
    }
}
