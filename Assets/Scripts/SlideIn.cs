using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideIn : MonoBehaviour
{


    [SerializeField]
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public float Duration = 1;
    public float Delay = 0;
    private float StartTime;

    private RectTransform rTransform;

    // Use this for initialization
    void Start()
    {
        rTransform = this.GetComponent<RectTransform>();
        if (rTransform != null)
        {
            rTransform.anchoredPosition3D = StartPosition;
        }
        else
        {
            transform.localPosition = StartPosition;
        }

        StartTime = Time.time + Delay;
    }

    // Update is called once per frame
    void Update()
    {
        float t = (Time.time - StartTime) / Duration;

        Vector3 pos = new Vector3(
                Mathf.SmoothStep(StartPosition.x, EndPosition.x, t),
                Mathf.SmoothStep(StartPosition.y, EndPosition.y, t),
                Mathf.SmoothStep(StartPosition.z, EndPosition.z, t));
        if (rTransform != null)
        {
            rTransform.anchoredPosition3D = pos;
        }
        else
        {
            transform.localPosition = pos;
        }
    }
}
