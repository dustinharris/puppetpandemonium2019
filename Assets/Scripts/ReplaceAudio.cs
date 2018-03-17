using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceAudio : MonoBehaviour
{
    [SerializeField]
    private bool Pause = false;

    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        GameObject[] musics = GameObject.FindGameObjectsWithTag("BGMusic");

        if (musics.Length > 1)
        {
            foreach (GameObject music in musics)
            {   
                if (!music.Equals(this.gameObject)) {
                    if (Pause)
                    {
                        audioSource = music.GetComponent<AudioSource>();
                        if (audioSource != null)
                        {
                            audioSource.Pause();
                        }
                    }
                    else
                    {
                        Destroy(music);
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (audioSource != null)
        {
            audioSource.UnPause();
        }
    }
}
