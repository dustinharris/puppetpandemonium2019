using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPitch : MonoBehaviour
{
    public float MinPitch = .1f;
    public float MaxPitch = .4f;

    public bool PlayOnEnable = true;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (PlayOnEnable)
        {
            PlayRandomPitch();
        }
    }

    public void PlayRandomPitch()
    {
        audioSource.pitch = (Random.Range(MinPitch, MaxPitch));
        audioSource.Play();
    }
}