using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformReader : MonoBehaviour {

    private LinkedList<GameObject> platforms;
    public List<string> platformsFromFile;
    public GameObject firstPlatform;
    public float platformScale = 1f;
    private Vector3 lastPlatformPosition;

    void Awake() {
        platforms = new LinkedList<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        
        // If there is at least one platform, check current position of last element in list.
        if(platforms.Count!=0)
        {
            GameObject LastObject = platforms.Last();
            lastPlatformPosition = LastObject.transform.position;
            // If there is about to be a gap on screen, load next Platform chunk
            // !!This needs to be relative to the camera position
            if (lastPlatformPosition.x <= 600)
            {
                //Debug.Log(lastPlatformPosition.x.ToString());

            }
        } else
        {
            // There is no ground yet
            Debug.Log("Spawned one ground");
            // Instantiate new object
            GameObject newObject = Instantiate(firstPlatform, new Vector3(9.32f, -1.53f, 7f), Quaternion.identity);
            // Update object scale
            newObject.transform.localScale = new Vector3(platformScale, platformScale, 1);
            // Save reference to object in platform array
            platforms.AddLast(newObject);
        }
	}
}
