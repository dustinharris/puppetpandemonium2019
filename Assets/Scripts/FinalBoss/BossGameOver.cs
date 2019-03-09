using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameOver : MonoBehaviour {

    [SerializeField]
    private SceneSwitcher sceneSwitcher;
    [SerializeField]
    private AudienceBarScript AudienceBar;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.BOSS_GAME_OVER, GameOver);
    }

    private void Start()
    {
        AudienceBar.ShowAll(AudienceUIScript.Notice.Glow);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.BOSS_GAME_OVER, GameOver);
    }

    private void GameOver()
    {
        sceneSwitcher.SwitchScenes();
    }
}
