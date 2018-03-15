using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRTimelineBehavior : MonoBehaviour {

    [SerializeField] private GameObject timelineCar;
    [SerializeField] private GameObject timelineCarMinY;
    [SerializeField] private GameObject timelineCarMaxY;
    [SerializeField] private GameObject gameCar;

    private Vector3 gameCarStartingPosition;
    private Vector3 gameCarEndPosition;
    private float timelineCarYPosition;
    private float timelineYTravelDistance;
    private float gameZTravelDistance;
    private float currentTimelineYPosition;
    private float currentGameProportionalZTravel;

    // Use this for initialization
    void Start () {
        // Position both TIMELINE cars at the beginning of the timeline
        float startingY = timelineCarMinY.transform.position.y;
        timelineCar.transform.position = new Vector3(timelineCar.transform.position.x, startingY, timelineCar.transform.position.z);

        // Get travel distance between the MinY and MaxY TIMELINE cars
        timelineYTravelDistance = timelineCarMaxY.transform.position.y - timelineCarMinY.transform.position.y;

        // Get starting and ending positions for red and blue GAME cars
        LRCarMovement carMovement = gameCar.GetComponent<LRCarMovement>();
        gameCarStartingPosition = carMovement.getCarStartingPosition();
        gameCarEndPosition = carMovement.getCarEndPosition();

        // Get travel distance between MinZ and MaxZ GAME cars
        gameZTravelDistance = Mathf.Abs(gameCarEndPosition.z - gameCarStartingPosition.z);
    }
	
	// Update is called once per frame
	void Update () {
        // Set position of TIMELINE cars based on progress of GAME cars
        // Current GAME car z poroportional travel
        currentGameProportionalZTravel = gameCar.transform.position.z - gameCarStartingPosition.z;

        // current gamecar z travel       current timeline y pos
        // ------------------------   =   -----------------------
        // total gamecar z travel         total timeline y travel
        // (current gamecar z travel)(total timeline y travel) = (total gamecar z travel)(current timeline y pos)
        // current timeline y pos = (current gamecar z travel)(total timeline y travel)/(total gamecar z travel)
        currentTimelineYPosition = timelineCarMinY.transform.position.y + (currentGameProportionalZTravel * timelineYTravelDistance) / gameZTravelDistance;

        // Update TIMELINE car position with new Y coordinate based on the position of GAME cars
        timelineCar.transform.position = new Vector3(timelineCar.transform.position.x, currentTimelineYPosition, timelineCar.transform.position.z);
    }
}
