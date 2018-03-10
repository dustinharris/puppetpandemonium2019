using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

    public interface IHealthSubscriber {
        void ZeroHealth();
    }

    [SerializeField]
    private Image HealthBar;
    [SerializeField]
    private IHealthSubscriber[] Subscribers;
    public int HitsToDead;

    private float width;
    private float perHit;

	void Start () {
        width = HealthBar.rectTransform.localScale.x;
        perHit = width / HitsToDead;
	}

    public void DecreaseHealth()
    {
        width -= perHit;
        if (width <= 0)
        {
            ZeroHealth();
        }
    }

    private void ZeroHealth()
    {
        for (int i = 0; i < Subscribers.Length; i++)
        {
            Subscribers[i].ZeroHealth();
        }
    }
}
