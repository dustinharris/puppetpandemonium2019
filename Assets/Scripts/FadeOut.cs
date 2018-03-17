using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

    public float Duration = 1;
    public float Delay = 0;
    private float StartTime;
    private Image image;
    private Text text;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        if (image == null)
        {
            text = GetComponent<Text>();
        }
        Color temp = GetColor();
        temp.a = 1;
        SetColor(temp);

        StartTime = Time.time + Delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > StartTime)
        {
            float t = (Time.time - StartTime) / Duration;
            Color temp = GetColor();
            temp.a = 1 - t;
            SetColor(temp);
        }
    }

    private Color GetColor()
    {
        if (image != null)
        {
            return image.color;
        }
        else
        {
            return text.color;
        }
    }

    private void SetColor(Color c)
    {
        if (image != null)
        {
            image.color = c;
        } else
        {
            text.color = c;
        }
    }
}
