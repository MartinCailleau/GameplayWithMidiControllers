using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class SerialReader : MonoBehaviour {
    SerialPort stream;
    public static SerialReader _instance;
    public string value;

    void Start () {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        stream = new SerialPort("COM4", 9600);
        stream.ReadTimeout = 50;
        stream.Open();
        
    }

    private void OnDisable()
    {
        stream.Close();
    }

    // Update is called once per frame
    void Update () {

        /* StartCoroutine
        (
            AsynchronousReadFromArduino
            ((string s) => value = s,     // Callback
                () => Debug.LogError("Error!"), // Error callback
                10000f                          // Timeout (milliseconds)
            )
        );*/

        Debug.Log(ReadFromArduino());
    }



    public string ReadFromArduino(int timeout = 0)
    {
        stream.ReadTimeout = timeout;
        try
        {
            return stream.ReadLine();
        }
        catch (TimeoutException e)
        {
            Debug.LogWarning(e);
            return null;
        }
    }

    public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);

        string dataString = null;

        do
        {
            try
            {
                dataString = stream.ReadLine();
            }
            catch (TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {
                callback(dataString);
                yield break; // Terminates the Coroutine
            }
            else
                yield return null; // Wait for next frame

            nowTime = DateTime.Now;
            diff = nowTime - initialTime;

        } while (diff.Milliseconds < timeout);

        if (fail != null)
            fail();
        yield return null;
    }
}
