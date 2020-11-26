using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioCollisionEffect: MonoBehaviour {
    public AudioSource audioSource;
    public AudioMixerSnapshot[] audioMixerSnapshots;

    private float[] snapshotWeights;
    private InputController input;


	// Use this for initialization
	void Start () {
        snapshotWeights = new float[audioMixerSnapshots.Length];
      //  input = GetComponent<InputController>();
      //  audioSource = GetComponent<AudioSource>();
      //  audioSource.clip = clip;
      //  audioSource.loop = true;
      //  audioSource.Play();
       
	}
	
	// Update is called once per frame
	void Update () {
       // audioSource.volume = input.getValue();
       // soundEffects();

    }

  /*  void soundEffects()
    {
        snapshotWeights[0] = 1-input.getValue();
        snapshotWeights[1] = input.getValue();
        audioSource.outputAudioMixerGroup.audioMixer.TransitionToSnapshots(audioMixerSnapshots, snapshotWeights, 0.1f);
    }*/

    void OnCollisionEnter()
    {
        StartCoroutine(playAudioEffect());
    }

    IEnumerator playAudioEffect()
    {
        snapshotWeights[0] = 0;
        snapshotWeights[1] = 1;
        audioSource.outputAudioMixerGroup.audioMixer.TransitionToSnapshots(audioMixerSnapshots, snapshotWeights, 0.1f);
        yield return new WaitForSeconds(0.5f);
        snapshotWeights[0] = 1;
        snapshotWeights[1] = 0;
        audioSource.outputAudioMixerGroup.audioMixer.TransitionToSnapshots(audioMixerSnapshots, snapshotWeights, 0.5f);

    }
}
