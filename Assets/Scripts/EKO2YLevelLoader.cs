using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EKO2YLevelLoader : MonoBehaviour {

    private LinkedList<GameObject> PlatformObjects;
    public HandleTextFile levelFileHandle;
    private List<string> platformsFromFileList;
    private Vector3 lastPlatformPosition;
    public float powerupScale;
    public float powerupYOffset;
    public float platformScale = 0f;
    private GameObject lastPlatformObject;
    private float lastPlatformX = 0f;

    [SerializeField] private float m_MaxSpeed = 10f;

    private int onListItem = 0;
    private float LowBlockYOffset = -2.18f;
    private float MidBlockYOffset = -1.2f;
    private float HighBlockYOffset = -.25f;
    private float blockWidth = .9f;

    public GameObject blocksEmpty;
    public GameObject groundBlockReference;
    public GameObject powerupReference;
    public GameObject sceneryEmpty;
    public GameObject cameraReference;
    public GameObject player1;

    private Vector2 movementSpeed;

    private Rigidbody2D block_Rigidbody2D;
    private Rigidbody2D scenery_Rigidbody2D;
    private Rigidbody2D player_Rigidbody2D;

    // Use this for initialization
    void Start () {
        PlatformObjects = new LinkedList<GameObject>();
        platformsFromFileList = levelFileHandle.ConvertLevelLayoutToList();

        player_Rigidbody2D = player1.GetComponent<Rigidbody2D>();

        block_Rigidbody2D = blocksEmpty.GetComponent<Rigidbody2D>();
        scenery_Rigidbody2D = sceneryEmpty.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        // If there is at least one platform, check current position of last element in list.
        if (PlatformObjects.Count != 0)
        {
            // Get the position of the last element
            lastPlatformObject = PlatformObjects.Last();
            lastPlatformPosition = new Vector3((lastPlatformObject.transform.position.x + blockWidth), 0, 0);

            // If there is about to be a gap on screen, load next Platform chunk
            if (lastPlatformPosition.x <= (cameraReference.transform.position.x + 5)) // !!This needs to be relative to the camera position
            {
                // Instantiate new object.
                // X position should be previous platform + one platform width

                if (platformsFromFileList[onListItem] == "0")
                {
                    // Create for player 1
                    GameObject newObject = Instantiate(groundBlockReference, new Vector3((lastPlatformPosition.x + blockWidth), LowBlockYOffset, 7f), Quaternion.identity);

                    // Update object scale;vary block color
                    newObject.transform.localScale = new Vector3(platformScale, platformScale, 1);
                    VaryBlockColor(newObject);
                    // Save reference to object in platform array
                    PlatformObjects.AddLast(newObject);
                    // if Player 1, set to player1BocksEmpty
                    newObject.transform.SetParent(blocksEmpty.transform);

                }
                else if (platformsFromFileList[onListItem] == "1")
                {
                    // Player 1
                    GameObject newObject = Instantiate(groundBlockReference, new Vector3((lastPlatformPosition.x + blockWidth), MidBlockYOffset, 7f), Quaternion.identity);
                    // Update object scale; vary block color
                    newObject.transform.localScale = new Vector3(platformScale, platformScale, 1);
                    VaryBlockColor(newObject);
                    // Save reference to object in platform array
                    PlatformObjects.AddLast(newObject);
                    // Set player1 object parent
                    newObject.transform.SetParent(blocksEmpty.transform);
                }
                else if (platformsFromFileList[onListItem] == "2")
                {
                    // Player 1
                    // Create bottom platform; update scale; vary block color
                    GameObject bottomPlatform = Instantiate(groundBlockReference, new Vector3((lastPlatformPosition.x + blockWidth), MidBlockYOffset, 7f), Quaternion.identity);
                    bottomPlatform.transform.localScale = new Vector3(platformScale, platformScale, 1);
                    VaryBlockColor(bottomPlatform);

                    // Create top platform; update scale; parent to first platform
                    // Create bottom platform; update scale; vary block color
                    GameObject topPlatform = Instantiate(groundBlockReference, new Vector3((lastPlatformPosition.x + blockWidth), HighBlockYOffset, 6.9f), Quaternion.identity);
                    topPlatform.transform.localScale = new Vector3(platformScale, platformScale, 1);
                    VaryBlockColor(topPlatform);

                    // Save reference to empty in platform array
                    PlatformObjects.AddLast(bottomPlatform);

                    // Set player1 object parent
                    bottomPlatform.transform.SetParent(blocksEmpty.transform);
                    topPlatform.transform.SetParent(blocksEmpty.transform);

                }
                else if (platformsFromFileList[onListItem] == "3")
                {
                    // player 1
                    GameObject newObject = Instantiate(groundBlockReference, new Vector3((lastPlatformPosition.x + blockWidth), LowBlockYOffset, 7f), Quaternion.identity);
                    // Update object scale; vary block color
                    newObject.transform.localScale = new Vector3(platformScale, platformScale, 1);
                    VaryBlockColor(newObject);
                    // Add powerup
                    GameObject powerupObject = Instantiate(powerupReference, new Vector3((lastPlatformPosition.x + blockWidth), newObject.transform.position.y + powerupYOffset, 7f), Quaternion.identity);
                    // Update powerup scale
                    powerupObject.transform.localScale = new Vector3(powerupScale, powerupScale, 1);
                    // Save reference to object in platform array
                    PlatformObjects.AddLast(newObject);

                    // Set player 1 object parent
                    newObject.transform.SetParent(blocksEmpty.transform);
                    powerupObject.transform.SetParent(blocksEmpty.transform);

                }
                else if (platformsFromFileList[onListItem] == "4")
                {

                    //Player 1
                    GameObject newObject = Instantiate(groundBlockReference, new Vector3((lastPlatformPosition.x + blockWidth), MidBlockYOffset, 7f), Quaternion.identity);
                    // Update object scale; vary block color
                    newObject.transform.localScale = new Vector3(platformScale, platformScale, 1);
                    VaryBlockColor(newObject);
                    // Save reference to object in platform array
                    PlatformObjects.AddLast(newObject);
                    // Add powerup
                    GameObject powerupObject = Instantiate(powerupReference, new Vector3((lastPlatformPosition.x + blockWidth), newObject.transform.position.y + powerupYOffset, 7f), Quaternion.identity);
                    // Update powerup scale
                    powerupObject.transform.localScale = new Vector3(powerupScale, powerupScale, 1);
                    // Set player1 object parent
                    newObject.transform.SetParent(blocksEmpty.transform);
                    powerupObject.transform.SetParent(blocksEmpty.transform);
                }
                else if (platformsFromFileList[onListItem] == "5")
                {
                    // Player 1
                    GameObject newObject = Instantiate(groundBlockReference, new Vector3((lastPlatformPosition.x + blockWidth), MidBlockYOffset, 7f), Quaternion.identity);
                    // Update object scale; vary block color
                    newObject.transform.localScale = new Vector3(platformScale, platformScale, 1);
                    VaryBlockColor(newObject);
                    // Save reference to object in platform array
                    PlatformObjects.AddLast(newObject);
                    // Add powerup
                    GameObject powerupObject = Instantiate(powerupReference, new Vector3((lastPlatformPosition.x + blockWidth), newObject.transform.position.y + powerupYOffset * 2, 7f), Quaternion.identity);
                    // Update powerup scale
                    powerupObject.transform.localScale = new Vector3(powerupScale, powerupScale, 1);

                    // Set player 1 object parent
                    newObject.transform.SetParent(blocksEmpty.transform);
                    powerupObject.transform.SetParent(blocksEmpty.transform);

                }
                else if (platformsFromFileList[onListItem] == "6")
                {
                    // Player 1
                    // Create bottom platform; update scale; vary block color
                    GameObject bottomPlatform = Instantiate(groundBlockReference, new Vector3((lastPlatformPosition.x + blockWidth), MidBlockYOffset, 7f), Quaternion.identity);
                    bottomPlatform.transform.localScale = new Vector3(platformScale, platformScale, 1);
                    VaryBlockColor(bottomPlatform);

                    // Create top platform; update scale; parent to first platform
                    // Create bottom platform; update scale; vary block color
                    GameObject topPlatform = Instantiate(groundBlockReference, new Vector3((lastPlatformPosition.x + blockWidth), HighBlockYOffset, 6.9f), Quaternion.identity);
                    topPlatform.transform.localScale = new Vector3(platformScale, platformScale, 1);
                    VaryBlockColor(topPlatform);

                    // Save reference to empty in platform array
                    PlatformObjects.AddLast(bottomPlatform);

                    // Add powerup
                    GameObject powerupObject = Instantiate(powerupReference, new Vector3((lastPlatformPosition.x + blockWidth), topPlatform.transform.position.y + powerupYOffset, 7f), Quaternion.identity);
                    // Update powerup scale
                    powerupObject.transform.localScale = new Vector3(powerupScale, powerupScale, 1);

                    // Set player 1 object parent
                    bottomPlatform.transform.SetParent(blocksEmpty.transform);
                    topPlatform.transform.SetParent(blocksEmpty.transform);
                    powerupObject.transform.SetParent(blocksEmpty.transform);


                }
                onListItem++;
                // Next time, load the following platform item from file
                //Debug.Log("On List Item: " + onListItem + " Count: " + platformsFromFileList.Count);
                if (onListItem >= platformsFromFileList.Count)
                {
                    onListItem = 0;
                }
            }
        }
        else
        {
            // There is no ground yet
            // Debug.Log("Spawned one ground");
            // Instantiate new object.

            // Player 1
            // X position should be same as character position
            GameObject newObject = Instantiate(groundBlockReference, new Vector3(player1.transform.position.x-3, MidBlockYOffset, 7f), Quaternion.identity);
            // Update object scale
            newObject.transform.localScale = new Vector3(platformScale, platformScale, 1);
            // Save reference to object in platform array
            PlatformObjects.AddLast(newObject);
            newObject.transform.SetParent(blocksEmpty.transform);
            onListItem++;
        }
    }

    private void FixedUpdate()
    {
        //movementSpeed = player_Rigidbody2D.velocity;
        // Move the blocks
        Move(1);
    }

    private void VaryBlockColor(GameObject block)
    {
        // Get references to all 3 color layers
        Transform peachLayer = block.transform.Find("Ground_Block_Peach");
        Transform pinkLayer = block.transform.Find("Ground_Block_Pink");
        Transform purpleLayer = block.transform.Find("Ground_Block_Purple");
        // Hide all 3 color layers
        peachLayer.GetComponent<Renderer>().enabled = false;
        pinkLayer.GetComponent<Renderer>().enabled = false;
        purpleLayer.GetComponent<Renderer>().enabled = false;

        float randomNumber = UnityEngine.Random.Range(0, 3);
        //Debug.Log("Random number: " + randomNumber.ToString());
        if ((randomNumber >= 0) && (randomNumber < 1))
        {
            // Peach block
            peachLayer.GetComponent<Renderer>().enabled = true;

        }
        else if ((randomNumber >= 1) && (randomNumber < 2))
        {
            // Pink block
            pinkLayer.GetComponent<Renderer>().enabled = true;

        }
        else
        {
            // Purple block
            purpleLayer.GetComponent<Renderer>().enabled = true;
        }
    }

    public void Move(float move)
    {
        // Move the blocks and scenery
        movementSpeed = new Vector2(-move * m_MaxSpeed, 0);
        block_Rigidbody2D.velocity = movementSpeed;
        scenery_Rigidbody2D.velocity = movementSpeed;
    }
}
