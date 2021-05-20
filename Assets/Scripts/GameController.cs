﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState { FreeRoam, Battle };
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
    }
    void StartBattle()
    {
        state = GameState.Battle;
        battleController.gameObject.SetActive(true);
        mainCam.gameObject.SetActive(false);

        battleController.StartBattle();

    }
    void EndBattle(bool Won)
    {
        state = GameState.FreeRoam;
        battleController.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
    }
}

