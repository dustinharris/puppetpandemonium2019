using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private Rigidbody2D block_Rigidbody2D;
        private Rigidbody2D scenery_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        public GameObject blocksEmpty;
        public GameObject groundBlockReference;
        public GameObject powerupReference;
        public int playerNumber;
        public GameObject blockEmpty;
        public GameObject sceneryEmpty;
        public GameObject cameraReference;
        private List<string> platformsFromFileList;
        public HandleTextFile levelFileHandle;
        public float powerupScale;
        public float powerupYOffset;
        public float platformScale = 0f;
        
        private int onListItem = 0;
        private float LowBlockYOffset = -.302f;
        private float MidBlockYOffset = 0.135f;
        private float HighBlockYOffset = .581f;

        private LinkedList<GameObject> PlatformObjects;

        private float blockWidth = .45f;
        private Vector3 lastPlatformPosition;

        bool doubleJump = false;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            block_Rigidbody2D = blockEmpty.GetComponent<Rigidbody2D>();
            scenery_Rigidbody2D = sceneryEmpty.GetComponent<Rigidbody2D>();
            PlatformObjects = new LinkedList<GameObject>();
            platformsFromFileList = levelFileHandle.ConvertLevelLayoutToList();
        }

        private void Start()
        {
            if (playerNumber == 1)
            {
                LowBlockYOffset = -.302f;
                MidBlockYOffset = 0.135f;
                HighBlockYOffset = .581f;
            } else
            {
                LowBlockYOffset = -2.5f;
                MidBlockYOffset = -2f;
                HighBlockYOffset = -1.5f;
            }
        }

        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            if(m_Grounded)
            {
                //Debug.Log("Reset double jump");
                doubleJump = false;
            }
        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                //m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // Character maintains its x position on screen
                m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
                transform.position = new Vector3(-.75f, transform.position.y, transform.position.z); 

                // Move the blocks
                block_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed * -1, 0);
                
                // If Player 1, Move the scenery
                if (playerNumber == 1)
                {
                    scenery_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed * -1, 0);
                }


                // If the input is moving the player right and the player is facing left...
                    if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            // Commented if statement is double jump only behavior.
            //if ((m_Grounded || !doubleJump) && jump)

            //Unlimited jump condition
            if ((!m_Grounded || !doubleJump) && jump)
            {
                //Debug.Log("Trying to jump");

                //transform.Rotate(0, 0, 30);

                // Add a vertical force to the player.
                m_Anim.SetBool("Ground", false);

                // Zero out y velocity
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);

                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

                if(!m_Grounded)
                {
                    // Used double jump
                    //Debug.Log("Used double jump");
                    doubleJump = true;
                }

                m_Grounded = false;

            }
        }

        private void Update()
        {
            // If there is at least one platform, check current position of last element in list.
            if (PlatformObjects.Count != 0)
            {
                lastPlatformPosition = new Vector3((PlatformObjects.Last().transform.position.x + blockWidth), 0, 0);

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
                GameObject newObject = Instantiate(groundBlockReference, new Vector3(transform.position.x, MidBlockYOffset, 7f), Quaternion.identity);
                // Update object scale
                newObject.transform.localScale = new Vector3(platformScale, platformScale, 1);
                // Save reference to object in platform array
                PlatformObjects.AddLast(newObject);
                newObject.transform.SetParent(blocksEmpty.transform);
                onListItem++;
            }
        }

        private void LateUpdate()
        {
            if (m_Rigidbody2D.velocity.y == 0) {
                transform.rotation = Quaternion.identity;
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
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
    }
}
