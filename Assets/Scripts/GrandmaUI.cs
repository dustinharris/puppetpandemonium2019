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

    [SerializeField]
    private GameObject TargetRedObject;
    [SerializeField]
    private GameObject TargetBlueObject;

    [SerializeField]
    private Sprite GrandmaTarget;
    [SerializeField]
    private Sprite GrandmaBroken;
    [SerializeField]
    private Sprite KittyTarget;
    [SerializeField]
    private Sprite KittyBroken;

    private TargetState[] Targets;
    private const int RED = 0;
    private const int BLUE = 1;

    private enum Target { Grandma, Kitty }

    private class TargetState
    {
        public Target which;
        public bool broken;
        public Image image;
    }

    private void Awake()
    {
        canvas = CanvasObject.GetComponent<Canvas>();

        BulletsRed = BulletsRedObject.GetComponentsInChildren<Image>();
        BulletsBlue = BulletsBlueObject.GetComponentsInChildren<Image>();

        Targets = new TargetState[2];
        Targets[RED].image = TargetRedObject.GetComponent<Image>();
        Targets[BLUE].image = TargetBlueObject.GetComponent<Image>();
    }

    public void ShowTargets()
    {
        Targets[RED].which = RandomTarget();
        Targets[BLUE].which = RandomTarget();
        Targets[RED].broken = false;
        Targets[BLUE].broken = false;

        Targets[RED].image.sprite = GetWholeSprite(Targets[RED].which);
        Targets[BLUE].image.sprite = GetWholeSprite(Targets[BLUE].which);

        TargetRedObject.SetActive(true);
        TargetBlueObject.SetActive(true);
    }

    private Target RandomTarget()
    {
        return (Target) Random.Range(0, 1);
    }

    private Sprite GetWholeSprite(Target target)
    {
        return (target == Target.Grandma) ? GrandmaTarget : KittyTarget;
    }

    private Sprite GetBrokenSprite(Target target)
    {
        return (target == Target.Grandma) ? GrandmaBroken : KittyBroken;
    }

    private bool TargetShootable(int index)
    {
        return !Targets[index].broken && Targets[index].image.IsActive();
    }

    private void BreakTarget(int index)
    {
        Targets[index].image.sprite = GetBrokenSprite(Targets[index].which);
        Targets[index].broken = true;
    }
	
    public void ShowAlert(bool red)
    {
        GameObject alert = Object.Instantiate(AlertImage) as GameObject;
        alert.transform.SetParent(canvas.transform);
        alert.transform.position = PlaceAlert(red);
        alert.transform.Rotate(RotateAlert());
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
    private Vector3 RotateAlert()
    {
        return new Vector3(0, 0, Random.Range(-45, 45));
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
        PuppetFired(RED, remaining);
    }

    public void BlueFired(int remaining)
    {
        PuppetFired(BLUE, remaining);
    }

    private void PuppetFired(int which, int remaining)
    {
        Image[] bulletImages;
        if (which == RED)
        {
            bulletImages = BulletsRed;
        } else
        {
            bulletImages = BulletsBlue;
        }
        DisplayLoadedBullets(bulletImages, remaining);

        if (TargetShootable(which))
        {
            BreakTarget(which);
        }
    }

    private void DisplayLoadedBullets(Image[] BulletImages, int remaining)
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
