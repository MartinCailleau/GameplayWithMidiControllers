using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseCollisionDetection : MonoBehaviour {
    private int hp = 2;
    public Gradient gradientColor;
	// Use this for initialization
	void Start () {
    //   EventManager.OnGameOver += gameOverForEvent;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (hp == 0)
        {
            StartCoroutine(gameOver());
        }
        else
        {
            StartCoroutine(takeDamage());
        }
        
    }

    IEnumerator takeDamage()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Collider>().enabled = false;
        --hp;
        Debug.Log("hp : "+ (1 - (hp / 2f)));
        Color c = gradientColor.Evaluate(1 - (hp / 2f));
        gameObject.GetComponent<Renderer>().material.SetColor("_Color",c);
        for (int i = 0;i<10;++i) { 
            yield return new WaitForSeconds(0.1f);
            c.a = 0.1f;
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", c);
            yield return new WaitForSeconds(0.1f);
            c.a = 1f;
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", c);
        }

       gameObject.GetComponent<Collider>().enabled = true;
    }

    IEnumerator gameOver()
    {

        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }
}
