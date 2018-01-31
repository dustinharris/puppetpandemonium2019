using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomObject : MonoBehaviour {

    public float zoomDistance;
    public float zoomSpeed;

    public void zoomOutIn()
    {
        zoomOut();
        zoomIn();
    }
        
    public void zoomOut()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, transform.position.y, zoomDistance);

        transform.position = Vector3.Lerp(startPosition, endPosition, zoomSpeed * Time.deltaTime);
    }

    public void zoomIn()
    {
        Vector3 endPosition = transform.position;
        Vector3 startPosition = new Vector3(transform.position.x, transform.position.y, (-1*zoomDistance));

        transform.position = Vector3.Lerp(startPosition, endPosition, zoomSpeed * Time.deltaTime);
    }
}
