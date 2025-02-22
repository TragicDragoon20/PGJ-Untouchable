﻿using UnityEngine;
using UnityEngine.Audio;

public class VoiceLine : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] voiceLines = null;
    private AudioSource audioSource = null;
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private AudioMixerGroup voiceVolume = null;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.OnVoiceTriggerEnter += PlayVoiceLine;
        audioSource = this.GetComponent<AudioSource>();
    }

    private void PlayVoiceLine(int id)
    {
        if (id == this.id)
        {
            audioSource.outputAudioMixerGroup = voiceVolume;
            audioSource.spatialBlend = 0.0f;
            audioSource.PlayOneShot(voiceLines[Random.Range(0, voiceLines.Length)]);
            this.id = 0;
        }
    }
}
