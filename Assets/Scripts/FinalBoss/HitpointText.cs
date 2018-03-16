using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitpointText : MonoBehaviour {

    [SerializeField] private GameObject hpText;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Color[] colors;

    void Awake()
    {
        Messenger.AddListener(GameEvent.BOSS_DECREASE_HP, BossDecreaseHP);
    }
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BossDecreaseHP()
    {
        // Get X, Y coordinates within Hitpoint Text object
        float newMinX = 0;
        float newMaxX = (this.GetComponent<RectTransform>().rect.width / 4);
        float newMinY = 0;
        float newMaxY = (this.GetComponent<RectTransform>().rect.height / 4);
        float newX = 350 + Random.Range(newMinX, newMaxX);
        float newY = 100 + Random.Range(newMinY, newMaxY);

        //CreateText(canvas, newX, newY, "-2000", 40, Color.white);

        // clone your prefab
        GameObject text = Instantiate(hpText, new Vector3(newX, newY, 0), Quaternion.identity);
        // set canvas as parent to the text
        text.transform.SetParent(canvas.transform);
        text.transform.position = new Vector3(newX, newY, 1);
        int randomColor = Random.Range(0, 3);
        if (randomColor == 0)
        {
            text.gameObject.GetComponent<Text>().color = Color.white;
        } else if (randomColor == 1)
        {
            text.gameObject.GetComponent<Text>().color = Color.red;
        } else
        {
            text.gameObject.GetComponent<Text>().color = Color.blue;
        }
    }

    GameObject CreateText(GameObject canvas, float x, float y, string text_to_print, int font_size, Color text_color)
    {
        GameObject UItextGO = new GameObject("Text2");
        UItextGO.transform.SetParent(canvas.gameObject.transform);

        RectTransform trans = UItextGO.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(x, y);

        Text text = UItextGO.AddComponent<Text>();
        text.text = text_to_print;
        text.fontSize = font_size;
        text.color = text_color;

        return UItextGO;
    }


}
