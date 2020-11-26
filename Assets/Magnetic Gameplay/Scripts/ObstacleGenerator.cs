using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reaktion;

public class ObstacleGenerator : MonoBehaviour {
    public GameObject prefab;
    public int nbPointMax,nbPointMini;
    public float offset;
    public float gapSize = 2;

    public bool isAudioReactif = false;
    public ReaktorLink reaktor;

    public bool useAudioAnalyseInput;

    public OSC osc;

    float waitime = 0;
    float time;
    private List<GameObject> obstacles;
    private int nbPoints;
    private int obstacleCounter = 0;
    private float audioAnalyseValue1 = 0;
    // Use this for initialization
    void Start () {
        reaktor.Initialize(this);
        obstacles = new List<GameObject>();
        createObstacle();
        setWaitingtime();
        osc.SetAddressHandler("/AudioParam1", setAudioAnalyzeValue1);

    }
	
    void setAudioAnalyzeValue1(OscMessage message)
    {
        audioAnalyseValue1 = message.GetFloat(0);
    }

    void createObstacle()
    {
        // 25 unités de hauteur
        if (isAudioReactif)
        {
            if (useAudioAnalyseInput)
            {
                nbPoints = (int)audioAnalyseValue1;
                Debug.Log(nbPoints);
            }
            else
            {
                Debug.Log("hauteur : " + (int)(reaktor.linkedObject.Output * 100) / (nbPointMax - nbPointMini));
                nbPoints = (int)(reaktor.linkedObject.Output * 100) / (nbPointMax - nbPointMini) + nbPointMini;
            }
           
        }else
        {
            nbPoints = Random.Range(nbPointMini, nbPointMax);
        }
        
        GameObject obstacle = new GameObject();
        for (int i = 0; i < nbPoints; ++i)
        {
            GameObject go = Instantiate(prefab, new Vector3(transform.position.x, 0, 1 + transform.position.z + offset * i), Quaternion.identity);
            go.transform.parent = obstacle.transform;
              
        }
        for(int i = 0; i < 25-(nbPoints+gapSize); ++i)
        {
            GameObject go = Instantiate(prefab, new Vector3(transform.position.x, 0, 1 + transform.position.z + offset * ((nbPoints+gapSize) + i)), Quaternion.identity);
            go.transform.parent = obstacle.transform;
            
        }
        
        Rigidbody rg = obstacle.AddComponent<Rigidbody>();
        rg.useGravity = false;
        rg.constraints = RigidbodyConstraints.FreezePosition;
        rg.constraints = RigidbodyConstraints.FreezeRotation;
        rg.AddForce(-Vector3.right * 200,ForceMode.Acceleration);
        Destroy(obstacle,20);
        obstacles.Add(obstacle);
        ++obstacleCounter;
    }

	// Update is called once per frame
	void FixedUpdate () {
        time += Time.deltaTime;
        if(waitime < time)
        {
            createObstacle();
            setWaitingtime();
            waitime += time;
        }
	}

    void setWaitingtime()
    {
        if((3 - obstacleCounter * 0.2f)>0)
        {// Progressif mode
            waitime = 3f - (obstacleCounter * 0.2f)+2f;
        }else
        {// Random hardcore mode
            waitime = Random.Range(2f,2.5f);
        }
        
    }
}
