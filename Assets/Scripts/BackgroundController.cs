using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float scrollSpeed = -1.5f;
    public float scaleFactor = .8f;
    private BoxCollider2D groundCollider;
    private float groundHorizontalLength;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(scrollSpeed, 0);
        groundCollider = GetComponent<BoxCollider2D>();
        groundHorizontalLength = groundCollider.size.x * scaleFactor;
        Debug.Log(groundHorizontalLength);
	}

    private void RepositionBackground()
    {
        Vector2 groundOffset = new Vector2(groundHorizontalLength * 2f, 0);
        transform.position = (Vector2)transform.position + groundOffset;
    }
	
	// Update is called once per frame
	void Update () {
        
        // Is it time to reposition bg?
        if (transform.position.x < -groundHorizontalLength)
        {
            RepositionBackground();
        }

    }
}
