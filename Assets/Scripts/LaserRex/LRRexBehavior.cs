using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRRexBehavior : MonoBehaviour {

    [SerializeField] private GameObject rexHeadPivot;
    [SerializeField] private float rexHeadWaitTime = 1f;
    private bool headLeft;

	// Use this for initialization
	void Start () {
        // Initialize values
        headLeft = true;

        // Go to next scene
        StartCoroutine(MoveHead());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator MoveHead()
    {
        if(headLeft)
        {
            // Head is currently left; move right
            rexHeadPivot.transform.rotation = Quaternion.Euler(-90f, -90f, 35f);
        } else
        {
            // Head is currently right; move left
            rexHeadPivot.transform.rotation = Quaternion.Euler(-90f, -90f, -35f);
        }
        // Update head position boolean
        headLeft = !headLeft;

        // Wait for X seconds, then call method again
        yield return new WaitForSeconds(rexHeadWaitTime);
        StartCoroutine(MoveHead());
    }
}
