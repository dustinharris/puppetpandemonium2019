using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    public AudioSource audioSource;
    public float MinPitch = .1f;
    public float MaxPitch = .4f;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()

    {
        PlayRandomPitch();





    }

    public void PlayRandomPitch()

    {

       
        audioSource.pitch = (Random.Range(MinPitch, MaxPitch));
        audioSource.Play();



    }

}