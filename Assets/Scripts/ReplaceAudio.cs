using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceAudio : MonoBehaviour {

    [SerializeField]
    private string AudioObjectName = "BGAudio";

	// Use this for initialization
	void Start () {
        GameObject oldAudio = GameObject.Find(AudioObjectName);
        if (oldAudio != null)
        {
            Destroy(oldAudio);
        }
	}
}
