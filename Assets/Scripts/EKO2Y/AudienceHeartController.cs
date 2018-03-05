using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceHeartController : MonoBehaviour {

    [SerializeField]
    private GameObject[] RedHearts;
    [SerializeField]
    private GameObject[] BlueHearts;

    public void ShowHeart(bool red, int index)
    {
        GameObject[] Hearts = red ? RedHearts : BlueHearts;
        GameObject heart = Hearts[index];

        if (heart.activeSelf)
        {
            heart.SetActive(false);
        }

        StartCoroutine(Show(heart));
    }

    private IEnumerator Show(GameObject heart)
    {
        heart.SetActive(true);
        yield return new WaitForSeconds(1f);
        heart.SetActive(false);
    }

}
