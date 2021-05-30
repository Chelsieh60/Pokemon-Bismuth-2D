using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState { FreeRoam, Battle, Dialog };
public class GameController : MonoBehaviour
{
    GameState state;

    [SerializeField] PlayerController playerController;
    [SerializeField] BattleController battleController;
    [SerializeField] Camera mainCam;

    private void Start()
    {
        playerController.OnEncounter += StartBattle;
        battleController.OnBattleOver += EndBattle;

        TextManager.instance.OnShowText += () =>
        {
            state = GameState.Dialog;
        };
        TextManager.instance.OnCloseText += () =>
        {
            if (state == GameState.Dialog)
            state = GameState.FreeRoam;
        };
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleController.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            TextManager.instance.HandleUpdate();
        }
    }
    void StartBattle()
    {
        state = GameState.Battle;
        battleController.gameObject.SetActive(true);
        mainCam.gameObject.SetActive(false);

        var playerParty = playerController.GetComponent < Party >();
        var wildPoke = FindObjectOfType<Map>().GetComponent<Map>().GetWildPokes();

        battleController.StartBattle(playerParty, wildPoke);

    }
    void EndBattle(bool Won)
    {
        state = GameState.FreeRoam;
        battleController.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
    }
}

