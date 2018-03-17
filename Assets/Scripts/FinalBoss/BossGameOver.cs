using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameOver : MonoBehaviour {

    [SerializeField]
    private SceneSwitcher sceneSwitcher;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.BOSS_GAME_OVER, GameOver);
    }

    private void GameOver()
    {
        sceneSwitcher.SwitchScenes();
    }
}
