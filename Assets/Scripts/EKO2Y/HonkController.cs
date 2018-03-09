using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonkController : MonoBehaviour {


    AudioSource[] Honks;
	// Use this for initialization
	void Start () {
        Honks = GetComponents<AudioSource>();
	}
	
    public void Honk()
    {
        int i = Random.Range(0, Honks.Length);

        Honks[i].Play();
    }
}
