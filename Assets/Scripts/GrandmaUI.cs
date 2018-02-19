using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrandmaUI : MonoBehaviour {

    [SerializeField]
    private GameObject CanvasObject;
    private Canvas canvas;
    [SerializeField]
    private GameObject AlertImage;
    [SerializeField]
    private GameObject ReloadRed;
    [SerializeField]
    private GameObject ReloadBlue;

    [SerializeField]
    private GameObject BulletsRedObject;
    [SerializeField]
    private GameObject BulletsBlueObject;

    private Image[] BulletsRed;
    private Image[] BulletsBlue;

    [SerializeField]
    private Sprite BulletLoaded;
    [SerializeField]
    private Sprite BulletGone;
    [SerializeField]
    private Sprite BulletReloading;

    private void Awake()
    {
        canvas = CanvasObject.GetComponent<Canvas>();

        BulletsRed = BulletsRedObject.GetComponentsInChildren<Image>();
        BulletsBlue = BulletsBlueObject.GetComponentsInChildren<Image>();
    }
	
    public void ShowAlert(bool red)
    {
        GameObject alert = Object.Instantiate(AlertImage) as GameObject;
        alert.transform.parent = canvas.transform;
        alert.transform.SetPositionAndRotation(PlaceAlert(red), RotateAlert());
        GameObject.Destroy(alert, 1.0f);
    }

    private Vector3 PlaceAlert(bool red)
    {
        Rect rect = canvas.pixelRect;
        
        float y = Random.Range(rect.yMin + 150, rect.yMax);
        float x;
        if (red)
        {
            x = Random.Range(rect.xMin, rect.xMax / 2);
        } else
        {
            x = Random.Range(rect.xMax / 2, rect.xMax);
        }

        return new Vector3(x, y);
    }
    private Quaternion RotateAlert()
    {
        return new Quaternion(0, 0, Random.Range(-45, 45), 0);
    }

    public void Reloading(bool red, int reloaded)
    {
        Image[] BulletImages = red ? BulletsRed : BulletsBlue;
        int remaining = GrandmaAmmo.MAG_SIZE - reloaded;

        int i;
        for (i = 0; i < remaining; i++)
        {
            Image bullet = BulletImages[i];
            bullet.sprite = BulletGone;
        }
        for (; i < GrandmaAmmo.MAG_SIZE; i++)
        {
            Image bullet = BulletImages[i];
            bullet.sprite = BulletReloading;
        }
    }

    public void RedFired(int remaining)
    {
        PuppetFired(BulletsRed, remaining);
    }

    public void BlueFired(int remaining)
    {
        PuppetFired(BulletsBlue, remaining);
    }

    private void PuppetFired(Image[] BulletImages, int remaining)
    {
        int i;
        for (i = 0; i < remaining; i++)
        {
            Image bullet = BulletImages[i];
            bullet.sprite = BulletLoaded;
        }
        for (; i < GrandmaAmmo.MAG_SIZE; i++)
        {
            Image bullet = BulletImages[i];
            bullet.sprite = BulletGone;
        }
    }

    public void OutOfAmmo(bool red)
    {
        if (red)
        {
            ReloadRed.SetActive(true);
        } else
        {
            ReloadBlue.SetActive(true);
        }
    }

    public void Reloaded(bool red)
    {
        if (red)
        {
            ReloadRed.SetActive(false);
        }
        else
        {
            ReloadBlue.SetActive(false);
        }

        Image[] BulletImages = red ? BulletsRed : BulletsBlue;
        for (int i = 0; i < GrandmaAmmo.MAG_SIZE; i++)
        {
            Image bullet = BulletImages[i];
            bullet.sprite = BulletLoaded;
        }
    }
}
