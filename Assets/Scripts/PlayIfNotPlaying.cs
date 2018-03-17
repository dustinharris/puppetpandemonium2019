using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIfNotPlaying : MonoBehaviour {

    [SerializeField]
    private float Volume = 1;

    private AudioSource NewAudio;

	// Use this for initialization
	void Start () {
        GameObject[] musics = GameObject.FindGameObjectsWithTag("BGMusic");

        if (musics.Length > 1) {
            foreach (GameObject music in musics) {
                if (!music.Equals(this.gameObject))
                {
                    AudioSource OldAudio = music.GetComponent<AudioSource>();
                    OldAudio.volume = Volume;

                    Destroy(this.gameObject);
                }
            }
        } else
        {
            NewAudio = GetComponent<AudioSource>();
            NewAudio.volume = Volume;
            NewAudio.Play();
        }
	}
}
