using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {

    public GameObject IntroText;
    public int onParagraph;
    public GameObject audioManager;
    public GameObject gameController;
    
    private void Awake()
    {
        DontDestroyOnLoad(audioManager);
        DontDestroyOnLoad(gameController);
    }

    // Use this for initialization
    void Start () {
        //int SpaceCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("On Scene: " + SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //On Scene 1 - Puppet Instructions
            //Debug.Log("show text");
            // Check if the space bar has a key up event in the last frame
            if (Input.GetKeyUp(KeyCode.Space))
            {
                // Increment SpaceCount;
                onParagraph+=1;
                Debug.Log("Current paragraph: " + onParagraph);

                // If final paragraph has been displayed, go to next scene
                // 1. Get count of Canvas children
                GameObject canvas = GameObject.Find("Canvas");
                int childrenCount = canvas.transform.childCount;
                if(onParagraph >= (childrenCount-1))
                {
                    Destroy(audioManager);
                    Debug.Log("GO TO NEXT SCENE");
                    SceneManager.LoadScene(1);
                } else
                {
                    // Hide previous paragraph
                    GameObject previousParagraph = GameObject.Find((onParagraph-1).ToString());
                    previousParagraph.GetComponent<Text>().enabled = false;

                    StartCoroutine(WaitASecond());
                }
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //On Scene 2 - InifiniteCute Logo Screen
            // Check if the space bar has a key up event in the last frame
            if (Input.GetKeyUp(KeyCode.Space))
            {
                DontDestroyOnLoad(GameObject.Find("Music"));
                SceneManager.LoadScene(2);
                Destroy(gameController);
            }
        }
    }

    IEnumerator WaitASecond()
    {
        print(Time.time);
        yield return new WaitForSeconds(.2f);
        print(Time.time);
        // Show current paragraph
        GameObject currentParagraph = GameObject.Find(onParagraph.ToString());
        currentParagraph.GetComponent<Text>().enabled = true;
    }
}
