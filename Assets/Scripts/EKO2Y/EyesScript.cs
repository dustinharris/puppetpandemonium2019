using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesScript : MonoBehaviour {

    public Sprite[] array;
    //public string sheetname;
    //private Sprite[] sprites;
    //private SpriteRenderer sr;
    //private string[] names;

    public float blinkMinTime = 1f;
    public float blinkMaxTime = 1f;

    // Use this for initialization
    void Awake()
    {
        NewEyes();
    }

    void Start () {
        //GetComponent<SpriteRenderer>().sprite = array[2];
        /*sprites = Resources.LoadAll<Sprite>(sheetname);
        sr = GetComponent<SpriteRenderer>();
        names = new string[sprites.Length];

        for (int i = 0; i < names.Length; i++)
        {
            names[i] = sprites[i].name;
        }*/
    }

    void NewEyes()
    {
        int randomSprite = Random.Range(0, array.Length - 1);
        //Debug.Log("Random sprite: " + randomSprite);
        GetComponent<SpriteRenderer>().sprite = array[randomSprite];
        //ChangeSprite(Random.Range(0, sprites.Length));
        Invoke("NewEyes", Random.Range(blinkMinTime, blinkMaxTime));
    }

    /**void ChangeSprite(int index)
    {
        Sprite sprite = sprites[index];
        sr.sprite = sprite;
    }

    void ChangeSpriteByName(string name)
    {
        Sprite sprite = sprites[System.Array.IndexOf(names, name)];
        sr.sprite = sprite;
    }**/
}
