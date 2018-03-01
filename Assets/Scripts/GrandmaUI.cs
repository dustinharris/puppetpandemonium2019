﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrandmaUI : MonoBehaviour
{

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

    [SerializeField]
    private GameObject HitRedObject;
    [SerializeField]
    private GameObject HitBlueObject;

    [SerializeField]
    private Sprite Scratch;
    [SerializeField]
    private Sprite Smooch;

    public int Lives;
    [SerializeField]
    private GameObject LivesObject;
    private Text LivesText;

    [SerializeField]
    private GameObject EndTextObject;
    private Text EndText;

    public string WinMessage = "You win!";
    public string LoseMessage = "You lose! :(";

    private SceneSwitcher sceneSwitcher;

    private GrandmaCatLives CatLives; 

    private TargetState[] Targets;
    private const int RED = 0;
    private const int BLUE = 1;

    private bool RedReady = false;
    private bool BlueReady = false;

    private enum Target { Grandma, Kitty }

    private class TargetState
    {
        public GameObject TargetObject;
        public GameObject HitObject;
        public Target which;
        public bool broken;
        public bool shootable;
        public Image image;
        public Image hit;
    }

    private void Awake()
    {
        canvas = CanvasObject.GetComponent<Canvas>();

        BulletsRed = BulletsRedObject.GetComponentsInChildren<Image>();
        BulletsBlue = BulletsBlueObject.GetComponentsInChildren<Image>();

        Targets = new TargetState[2];
        Targets[RED] = new TargetState();
        Targets[BLUE] = new TargetState();
        Targets[RED].TargetObject = TargetRedObject;
        Targets[BLUE].TargetObject = TargetBlueObject;
        Targets[RED].HitObject = HitRedObject;
        Targets[BLUE].HitObject = HitBlueObject;
        Targets[RED].image = TargetRedObject.GetComponent<Image>();
        Targets[BLUE].image = TargetBlueObject.GetComponent<Image>();
        Targets[RED].hit = HitRedObject.GetComponent<Image>();
        Targets[BLUE].hit = HitBlueObject.GetComponent<Image>();
        Targets[RED].shootable = false;
        Targets[BLUE].shootable = false;

        LivesText = LivesObject.GetComponent<Text>();
        EndText = EndTextObject.GetComponent<Text>();

        CatLives = GetComponent<GrandmaCatLives>();
        sceneSwitcher = GetComponent<SceneSwitcher>();
    }

    void Start()
    {
        LivesText.text = Lives.ToString();

        StartCoroutine(WaitForReady());
    }

    private IEnumerator WaitForReady()
    {
        while (!RedReady || !BlueReady)
        {
            yield return null;
        }

        ShowTargets();
    }

    private IEnumerator WaitToTimeout()
    {
        float time = Time.time;
        while (true)
        {
            if (BothBroken())
            {
                Debug.Log("Both targets broken");
                StartCoroutine(NextCycle());
                break;
            }
            else if (Time.time - time > 3f)
            {
                Timeout();
                break;
            }

            yield return null;
        }
    }

    // Temp function that calls ShowTargets until camera is hooked up
    private IEnumerator WaitToShow()
    {
        yield return new WaitForSeconds(3f);
        ShowTargets();
    }

    public void ShowTargets()
    {
        Debug.Log("ShowTargets");
        Targets[RED].which = RandomTarget();
        Targets[BLUE].which = RandomTarget();
        Targets[RED].broken = false;
        Targets[BLUE].broken = false;
        Targets[RED].shootable = true;
        Targets[BLUE].shootable = true;

        Targets[RED].image.sprite = GetWholeSprite(Targets[RED].which);
        Targets[BLUE].image.sprite = GetWholeSprite(Targets[BLUE].which);

        Targets[RED].TargetObject.SetActive(true);
        Targets[BLUE].TargetObject.SetActive(true);
        Targets[RED].HitObject.SetActive(false);
        Targets[BLUE].HitObject.SetActive(false);

        StartCoroutine(WaitToTimeout());
    }

    private void HideTargets()
    {
        Targets[RED].shootable = false;
        Targets[BLUE].shootable = false;

        SetTargetActive(RED, false);
        SetTargetActive(BLUE, false);
    }

    private void SetTargetActive(int which, bool active)
    {
        Targets[which].TargetObject.SetActive(active);
    }

    private void SetHitActive(int which, bool active)
    {
        Targets[which].HitObject.SetActive(active);
    }

    private bool GetTargetActive(int which)
    {
        return Targets[which].TargetObject.activeSelf;
    }

    private void Timeout()
    {
        Targets[RED].shootable = false;
        Targets[BLUE].shootable = false;

        if (!Targets[RED].broken)
        {
            Hit(RED);
        }
        if (!Targets[BLUE].broken)
        {
            Hit(BLUE);
        }

        StartCoroutine(NextCycle());
    }

    private void Hit(int which)
    {
        Target target = Targets[which].which;
        Targets[which].hit.sprite = GetTargetHit(target);
        SetHitActive(which, true);
        if (target == Target.Kitty)
        {
            if (Lives > 0)
            {
                Lives--;
            }
            LivesText.text = Lives.ToString();
        }
    }

    private Sprite GetTargetHit(Target which)
    {
        return which == Target.Grandma ? Smooch : Scratch;
    }

    private IEnumerator NextCycle()
    {
        yield return new WaitForSeconds(1f);
        HideTargets();

        if (CatLives.GetCatLives() <= 0)
        {
            StartCoroutine(EndGame(SceneSwitcher.Result.Win));
        } else if (Lives <= 0)
        {
            StartCoroutine(EndGame(SceneSwitcher.Result.Lose));
        } else
        {
            StartCoroutine(WaitToShow());
        }
    }

    private IEnumerator EndGame(SceneSwitcher.Result result)
    {
        string message;
        if (result == SceneSwitcher.Result.Win)
        {
            message = WinMessage;
        } else
        {
            message = LoseMessage;
        }

        EndText.text = message;
        EndTextObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        sceneSwitcher.result = result;
        sceneSwitcher.SwitchScenes();
    }

    private bool BothBroken()
    {
        return Targets[RED].broken && Targets[BLUE].broken;
    }

    private Target RandomTarget()
    {
        int i = Random.Range(0, 2);
        return (Target) i;
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
        return Targets[index].shootable && !Targets[index].broken && GetTargetActive(index);
    }

    private void BreakTarget(int index)
    {
        if (Targets[index].which == Target.Kitty)
        {
            CatLives.DecreaseCatLives();
        }

        Targets[index].image.sprite = GetBrokenSprite(Targets[index].which);
        Targets[index].broken = true;
        Targets[index].shootable = false;
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
        }
        else
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
        }
        else
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
        }
        else
        {
            ReloadBlue.SetActive(true);
        }
    }

    public void Reloaded(bool red)
    {
        if (red)
        {
            ReloadRed.SetActive(false);
            RedReady = true;
        }
        else
        {
            ReloadBlue.SetActive(false);
            BlueReady = true;
        }

        Image[] BulletImages = red ? BulletsRed : BulletsBlue;
        for (int i = 0; i < GrandmaAmmo.MAG_SIZE; i++)
        {
            Image bullet = BulletImages[i];
            bullet.sprite = BulletLoaded;
        }
    }
}
