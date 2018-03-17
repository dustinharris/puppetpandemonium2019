using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosions : MonoBehaviour {

    [SerializeField] private SceneSwitcher sceneSwitcher;
    [SerializeField] private GameObject White;
    [SerializeField] private GameObject[] ExplosionObjects;

	// Use this for initialization
	void Start () {
        Messenger.AddListener(GameEvent.ALL_GO, AllGo);
	}
	
	private void AllGo()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        for (int i = 0; i < ExplosionObjects.Length; i++) {
            ExplosionObjects[i].SetActive(true);
            if (i < ExplosionObjects.Length - 2)
            {
                yield return new WaitForSeconds(.5f);

                if (i == ExplosionObjects.Length - 3)
                {
                    White.SetActive(true);
                }
            } else
            {
                
                yield return new WaitForSeconds(1f);
            }
        }

        

        yield return new WaitForSeconds(3f);
        sceneSwitcher.SwitchScenes();
    }
}
