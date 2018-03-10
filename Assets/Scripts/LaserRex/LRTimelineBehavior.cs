using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRTimelineBehavior : MonoBehaviour {

    [SerializeField] private GameObject redTimelineCar;
    [SerializeField] private GameObject blueTimelineCar;
    [SerializeField] private GameObject timelineCarMinY;
    [SerializeField] private GameObject timelineCarMaxY;
    [SerializeField] private GameObject redGameCar;
    [SerializeField] private GameObject blueGameCar;

    private Vector3 redGameCarStartingPosition;
    private Vector3 blueGameCarStartingPosition;
    private Vector3 redGameCarEndPosition;
    private Vector3 blueGameCarEndPosition;
    private float redTimelineCarYPosition;
    private float blueTimelineCarYPosition;
    private float timelineYTravelDistance;
    private float gameZTravelDistance;
    private float currentRedTimelineYPosition;
    private float currentBlueTimelineYPosition;
    private float currentRedGameProportionalZTravel;
    private float currentBlueGameProportionalZTravel;

    // Use this for initialization
    void Start () {
        // Position both TIMELINE cars at the beginning of the timeline
        float startingY = timelineCarMinY.transform.position.y;
        redTimelineCar.transform.position = new Vector3(redTimelineCar.transform.position.x, startingY, redTimelineCar.transform.position.z);
        blueTimelineCar.transform.position = new Vector3(blueTimelineCar.transform.position.x, startingY, blueTimelineCar.transform.position.z);

        // Get travel distance between the MinY and MaxY TIMELINE cars
        timelineYTravelDistance = timelineCarMaxY.transform.position.y - timelineCarMinY.transform.position.y;

        // Get starting and ending positions for red and blue GAME cars
        redGameCarStartingPosition = redGameCar.GetComponent<LRCarMovement>().getCarStartingPosition();
        blueGameCarStartingPosition = blueGameCar.GetComponent<LRCarMovement>().getCarStartingPosition();
        redGameCarEndPosition = redGameCar.GetComponent<LRCarMovement>().getCarEndPosition();
        blueGameCarEndPosition = blueGameCar.GetComponent<LRCarMovement>().getCarEndPosition();

        // Get travel distance between MinZ and MaxZ GAME cars
        gameZTravelDistance = Mathf.Abs(redGameCarEndPosition.z - redGameCarStartingPosition.z);
    }
	
	// Update is called once per frame
	void Update () {
        // Set position of TIMELINE cars based on progress of GAME cars
        // Current GAME car z poroportional travel
        currentRedGameProportionalZTravel = redGameCar.transform.position.z - redGameCarStartingPosition.z;
        currentBlueGameProportionalZTravel = blueGameCar.transform.position.z - blueGameCarStartingPosition.z;

        // current gamecar z travel       current timeline y pos
        // ------------------------   =   -----------------------
        // total gamecar z travel         total timeline y travel
        // (current gamecar z travel)(total timeline y travel) = (total gamecar z travel)(current timeline y pos)
        // current timeline y pos = (current gamecar z travel)(total timeline y travel)/(total gamecar z travel)
        currentRedTimelineYPosition = timelineCarMinY.transform.position.y + (currentRedGameProportionalZTravel * timelineYTravelDistance) / gameZTravelDistance;
        currentBlueTimelineYPosition = timelineCarMinY.transform.position.y + (currentBlueGameProportionalZTravel * timelineYTravelDistance) / gameZTravelDistance;

        // Update TIMELINE car position with new Y coordinate based on the position of GAME cars
        redTimelineCar.transform.position = new Vector3(redTimelineCar.transform.position.x, currentRedTimelineYPosition, redTimelineCar.transform.position.z);
        blueTimelineCar.transform.position = new Vector3(blueTimelineCar.transform.position.x, currentBlueTimelineYPosition, blueTimelineCar.transform.position.z);
    }
}
