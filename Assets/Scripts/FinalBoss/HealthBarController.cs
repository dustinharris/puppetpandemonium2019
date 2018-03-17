using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

    [SerializeField]
    private Image HealthBar;
    [SerializeField]
    public int HitsToDead;
    [SerializeField] private GameObject controller;

    private float width;
    private float perHit;

	void Start () {
        width = HealthBar.rectTransform.localScale.x;
        perHit = width / HitsToDead;
	}

    public void DecreaseHealth()
    {
        width -= perHit;
        HealthBar.rectTransform.localScale = new Vector3(width, HealthBar.rectTransform.localScale.y);
        if (width <= 0)
        {
            ZeroHealth();
        }
    }

    private void ZeroHealth()
    {
        Messenger.Broadcast(GameEvent.BOSS_GAME_OVER);
    }
}
