using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizedPosition : MonoBehaviour {

    [SerializeField]
    private GameObject SynchronizationObject;

	void Update () {
        transform.localPosition = SynchronizationObject.transform.localPosition;
	}
}
