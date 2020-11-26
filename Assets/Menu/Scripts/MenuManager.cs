using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    float returnKeyPressCounter = 0;
    float timeRequired = 1;
    bool startKeyPressCounter;

    static MenuManager instance;

    // Use this for initialization
    void Start () {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        backToMenu();
    }

    public void launchLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    
    private void backToMenu()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            startKeyPressCounter = true;
            Debug.Log("backspace");
        }
        if (startKeyPressCounter)
        {
            returnKeyPressCounter += Time.deltaTime;
            if(returnKeyPressCounter >= timeRequired)
            {
                startKeyPressCounter = false;
                returnKeyPressCounter = 0;
                SceneManager.LoadScene("MainMenu");

            }
        }
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            startKeyPressCounter = false;
            returnKeyPressCounter = 0;
        }

    }
}
