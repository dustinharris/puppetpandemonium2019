using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAttachment : MonoBehaviour
{

    private AudioSource AudioSource;

    // Use this for initialization
    void Start()
    {

        AudioSource = GetComponent<AudioSource>();
        

    }


    private void OnEnable()
    {
        Reset();
    }

    private void Reset()
    {
        this.AudioSource.Play();

    }


}
