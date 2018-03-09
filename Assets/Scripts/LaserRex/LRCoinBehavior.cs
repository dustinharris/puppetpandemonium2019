using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRCoinBehavior : MonoBehaviour {

    [SerializeField] private float coinDuration = 3f;
    private GameObject sceneryObject;

    private void Start()
    {
        // Call coroutine to wait X seconds, then destroy coin.
        // Desirable for performance reasons.
        StartCoroutine(DestroyCoin(coinDuration));

        // Find scenery empty
        sceneryObject = GameObject.Find("Scenery_Empty");
    }

    private IEnumerator DestroyCoin(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Road")
        {
            this.gameObject.transform.SetParent(sceneryObject.transform);
        }
    }
}
