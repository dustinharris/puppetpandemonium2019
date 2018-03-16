using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpTextbehavior : MonoBehaviour {

    [SerializeField] private float textDuration = .5f;

    // Use this for initialization
    void Start () {
        StartCoroutine(DestroyText(textDuration));
    }

    private IEnumerator DestroyText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
