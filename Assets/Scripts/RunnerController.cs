using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RunnerController : MonoBehaviour {

    public HandleTextFile levelFileHandle;
    private List<string> platformsFromFileList;
    //private LinkedList<GameObject> player2PlatformObjects;
    public GameObject player1BlocksEmpty;
    public GameObject player2BlocksEmpty;
    public GameObject sceneryEmpty;
    public GameObject groundBlockReference;
    public GameObject powerupReference;
    private float blockWidth = .45f;
    public float powerupScale;
    public float powerupYOffset;
    public float platformScale = 0f;
    public GameObject character;
    public GameObject cameraReference;
    private Vector3 lastPlatformPosition;
    private int onListItem = 0;
    private float player1LowBlockYOffset = -.302f;
    private float player1MidBlockYOffset = 0.135f;
    private float player1HighBlockYOffset =.581f;
    private float player2LowBlockYOffset = -2.5f;
    private float player2MidBlockYOffset = -2f;
    private float player2HighBlockYOffset = -1.5f;

    void Awake()
    {
        // Convert level layout to list
        //platformsFromFileList = levelFileHandle.ConvertLevelLayoutToList();   // OKlevelFileHandle.ConvertLevelLayoutToArray();
        // Create list for platform gameObjects
        //player1PlatformObjects = new LinkedList<GameObject>();
        //player2PlatformObjects = new LinkedList<GameObject>();
    }
    
    // Use this for initialization
    void Start () {
    }

    /**public LinkedList<GameObject> getPlayer1Blocks ()
    {
        return player1PlatformObjects;
    }

    public LinkedList<GameObject> getPlayer2Blocks()
    {
        return player2PlatformObjects;
    }**/

    // Update is called once per frame
    void Update () {
        
    }

    public void LoadGameOverScene ()
    {
        SceneManager.LoadScene(2);
    }

    
}
