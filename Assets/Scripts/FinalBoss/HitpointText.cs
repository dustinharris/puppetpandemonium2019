using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitpointText : MonoBehaviour {

    [SerializeField] private GameObject hpText;

    void Awake()
    {
        Messenger.AddListener(GameEvent.BOSS_DECREASE_HP, BossDecreaseHP);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.BOSS_DECREASE_HP, BossDecreaseHP);
    }

    private void BossDecreaseHP()
    {
        RectTransform rect = this.GetComponent<RectTransform>();

        // Get X, Y coordinates within Hitpoint Text object
        float newMinX = 0;
        float newMaxX = rect.rect.width;
        float newMinY = 0;
        float newMaxY = rect.rect.height;
        float newX = Random.Range(newMinX, newMaxX);
        float newY = Random.Range(newMinY, newMaxY);

        // clone your prefab
        GameObject text = Instantiate(hpText, Vector3.one, Quaternion.identity, rect);

        RectTransform textRect = text.GetComponent<RectTransform>();
        textRect.anchoredPosition = new Vector3(newX, newY, 1);
    }
}
