using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDownTimer : MonoBehaviour
{
    public float timeLeft = 0.0f;

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        timeLeft  -= Time.deltaTime;
        text.text =  "IMPACT" + timeLeft ;
      
    }
}