using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilation : MonoBehaviour {
    public GameObject prefab;
    public int nbPoints;
    public float offset;

    public bool activeOscSend;
    public OSC osc;

    private List<Transform> points;
    private InputController input;
	// Use this for initialization
	void Start () {
        input = GetComponent<InputController>();
        points = new List<Transform>();
        for(int i =0; i<nbPoints; ++i)
        {
            GameObject go = Instantiate(prefab,new Vector3(1 + transform.position.x + offset * i,0,transform.position.z ),Quaternion.identity);
            go.transform.parent = transform;
            points.Add(go.transform);
        }
	}

    private int i;
    private float verticalMove;
    public float phase;
    public float periode;
    public float periodeMini = -2f,periodeMaxi = 2f;
    public float amplitude = 1;
	// Update is called once per frame
	void Update () {
        i = 0;

        periode = (input.getValue() * 2) - 1; 
        Debug.Log(periode);
        foreach (Transform p in points)
        {
            verticalMove = phase + (i * periode);
            p.localPosition = new Vector3(p.localPosition.x,0,Mathf.Cos(verticalMove+Time.time)*amplitude);
            ++i;
        }
        if (activeOscSend)
        {
            OscMessage message = new OscMessage();
            message.address = "/Oscilation_"+gameObject.name;
            message.values.Add(verticalMove);
            osc.Send(message);

        }
	}

}
