using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceAudio : MonoBehaviour {

    [SerializeField]
    private string AudioObjectName = "BGAudio";
    [SerializeField]
    private bool Pause = false;

    private AudioSource audio;

	// Use this for initialization
	void Start () {
        GameObject oldAudio = GameObject.Find(AudioObjectName);
        if (oldAudio != null)
        {
            if (Pause)
            {
                audio = oldAudio.GetComponent<AudioSource>();
                if (audio != null)
                {
                    audio.Pause();
                }
            }
            else
            {
                Destroy(oldAudio);
            }
        }
	}

    private void OnDestroy()
    {
        if (audio != null)
        {
            audio.UnPause();
        }
    }
}
