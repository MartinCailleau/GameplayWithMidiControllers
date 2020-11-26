using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reaktion;
using MidiJack;

public enum InputMode { AxisMode, ButtonMode }
public class InputController : MonoBehaviour {
    public InputMode inputMode;
    public int midiNoteAxis;
    public string unityAxis;
    public int midiNoteUp,midiNoteDown;
    public KeyCode buttonUp,buttonDown;
    
    private float value;
    private float unityAxisValue;
    private float midiValue;
    // Use this for initialization
    void Start () {
        value = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

            switch(inputMode)
            {
                case InputMode.AxisMode :
                    unityAxisValue = (unityAxis != "")?Input.GetAxis(unityAxis):0;
                    midiValue = MidiMaster.GetKnob(MidiChannel.All, midiNoteAxis);
                    Debug.Log(midiValue);
                    if (unityAxis == "" || (unityAxisValue < 0.1f && unityAxisValue > -0.1f))
                    {
                        value = midiValue;
                    }
                    else
                    {
                        value = unityAxisValue;
                    }
                break;
                case InputMode.ButtonMode:
                    if (MidiMaster.GetKnob(MidiChannel.All,midiNoteUp)>0 || Input.GetKey(buttonUp))
                    {
                        value = Mathf.Lerp(value, 1, 1f * Time.deltaTime);
                    }
                    if((MidiMaster.GetKnob(MidiChannel.All, midiNoteDown) > 0) || Input.GetKey(buttonDown))
                    {
                        value = Mathf.Lerp(value, 0, 1f * Time.deltaTime);
                    }
                break;
                default: break;
            }
            
            
        
       


      //  Debug.Log("Get Button Down "+MidiMaster.GetKeyDown(MidiChannel.All, midiNote));

      //  Debug.Log("Get Note Value " + MidiMaster.GetKnob(MidiChannel.All, midiNote));

    }

    public float getValue()
    {
        return value;
    }
}
